# Laboratory Work No.2

## Multithreading Programming

### Task
- Create a program that would use Event-Driven Concurrency to _"recreate the situation"_ below:

+ Meaning: __4__ is dependent of the concurrent events __5-7__, and __1-3__ are dependent of __4__, which means that until those are not finished, the dependent threads cannot be executed.

![img](https://user-images.githubusercontent.com/22482507/52741749-cbcd8600-2fde-11e9-8547-d8fdc9260a9b.JPG)   

---

### Solution

_(Since the code is small, I deem it appropriate to paste here)_

__Language__: C#

```cs
AutoResetEvent[] autoResetEvents = {
  new AutoResetEvent(false),
  new AutoResetEvent(false),
  new AutoResetEvent(false)
};
var lastEvent = new ManualResetEvent(false);

var threads = new List<Thread>
{
  new Thread(() => { Console.WriteLine("7"); autoResetEvents[0].Set(); }),
  new Thread(() => { Console.WriteLine("6"); autoResetEvents[1].Set(); }),
  new Thread(() => { Console.WriteLine("5"); autoResetEvents[2].Set(); }),
  new Thread(() => { WaitHandle.WaitAll(autoResetEvents);
                Console.WriteLine("4"); lastEvent.Set(); }),
  new Thread(() => { lastEvent.WaitOne(); Console.WriteLine("3"); }),
  new Thread(() => { lastEvent.WaitOne(); Console.WriteLine("2"); }),
  new Thread(() => { lastEvent.WaitOne(); Console.WriteLine("1"); })
};

threads.ForEach(x => x.Start());
Console.ReadKey();
```

- I have 6 threads and an array of _AutoResetEvents_ set to `false`.

- The first 3 threads (__5-7__) after they're executed, each set an _AutoResetEvent_ from the array to `true`.

- The __4th__ thread is waiting for all of those to be set to `true`, after that it can be executed.

  + After it finishes its task, a _ManualResetEvent_ (which is `false` until this) is set to `true`.


- The last threads (__1-3__) are waiting for that _ManualResetEvent_ to be set to `true`, and then execute.

  + A _ManualResetEvent_ is used instead of an _AutoResetEvent_ because it allows multiple threads to continue, whereas the 2nd one only allows one thread at a time, and we don't want that.


- This way, all these threads are executed in some kind of order (except the concurrent ones). First the __5-7__, then __4__ and in the end the __1-3__.
