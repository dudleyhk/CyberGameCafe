HOW-TO GIT:
basics

- Create a new branch and edit on that branch before commiting to master
- DO NOT COMMIT A BROKEN BUILD TO MASTER
- If you're working in a different branch pull from master regularly otherwise everything will fuck up your end and its a nightmare.

Google is your friend but ask if another issue arises.


To Open:
- Right click the folder where the .git file is (if you can't see this its probably hidden... Unhide it).
- Open GIT Bash

To Clone:
- Instructions are on GITHUB
- git clone "address"

To Pull:
git pull origin branchname

To Push:
git add .
git commit -m "you have to put a useful message here other wise reverting the project because difficult"
git push -u origin branchname

Make New Branch:
git branch branchname

Goto Branch:
git checkout branchname
