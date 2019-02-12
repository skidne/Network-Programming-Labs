## Git basics overview

- Initializing an empty git repo
- Generating and using an SSH key
- Adding a remote repo
- Creating and merging branches

---

## Used git subcommands

```sh
$ git init #initializes a local git repo
$ echo a > kek & echo aa > kek2 #redirects output to create files
$ git add * #adds all the changes to be committed later
$ git commit -m "first commit" #commits all the changes
$ git remote add origin git@github.com:skidne/Network-Programming-Labs.git #adds a remote repo to be stored on a server (github here)
$ git push -u origin master # pushes all the changes to the repo
$ git checkout -b dev # creates and moves to a new branch
$ echo aaa > kekdev #creates a file
$ git add *
$ git commit -m "dev commit" #commits the changes to the new branch
$ git push -u origin dev #pushes to the new branch
$ git checkout master #moves to the master branch
$ echo aaaa > kekmaster
$ git add *
$ git commit -m "master commit" #commits the changes to master
$ git push -u origin master
$ git merge dev #merges dev and master branches
$ git commit -m "merging" #commits the merge
$ git push -u origin master #final push :00
```
---

Changes were made.

To see how the commands above were executed, check the commits.
