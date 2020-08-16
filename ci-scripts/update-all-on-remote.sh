echo "WARNING: This script all update ALL existing branches according to the remote information for the origin remote ONLY. This may break other people's branches or create merge conflicts! Only proceed if you underatand the implications!"
read -r -p "Do you understand the implications and wish to continue? (y/n)" input

case $input in
    [yY][eE][sS]|[yY])
        git checkout development
        git pull --ff-only
        
        originPrefix="origin/"
        
        for branch in $(git for-each-ref --format="%(refname:short)"); do
            echo "INFO: Attempting rebase of ${branch}"
            newBranch=${branch#"$originPrefix"}
            git checkout $newBranch
            git rebase development
            git push
        done
    ;;
    [nN][oO]|[nN])
        echo "No"
    ;;
    *)
        echo "Invalid input..."
        exit 1
    ;;
esac

