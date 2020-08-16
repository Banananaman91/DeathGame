set -e

echo "WARNING: This script will update ALL existing branches according to the remote information for the origin remote ONLY. This may break other people's branches or create merge conflicts! Only proceed if you understand the implications!"
read -r -p "Do you understand the implications and wish to continue? (y/n)" input

case $input in
    [yY][eE][sS]|[yY])
        git checkout development
        git pull --ff-only
        
        originPrefix="origin/"
        
        for branch in $(git for-each-ref --format="%(refname:short)"); do
            newBranch=${branch#"$originPrefix"}

            if [ "${newBranch}" != "master" ] && [ "${newBranch}" != "development" ]; then
                echo "INFO: Attempting rebase of ${branch}"
                git checkout "${newBranch}"
                git pull
                git rebase development
                git push
            fi
        done
    ;;
    [nN][oO]|[nN])
        exit 0
    ;;
    *)
        echo "Invalid input. Exiting."
        exit 1
    ;;
esac

