#!/bin/bash
echo branch: $TRAVIS_BRANCH
echo "start restore"
#dotnet restore ./src/WebPlatform.sln
echo "restore done"

echo "start build"
#dotnet build ./src/WebPlatform.sln
echo "build done"

function test(){
	local a=25
    local b=3
	local c=$((a+b))
	echo $c
}
test

function get_docker_tag(){
	echo $TRAVIS_BRANCH 
	echo $TRAVIS_COMMIT
	if [ $1 == "development" ]; then
		return $"development-{$2}"
	else
		return $"production-{$2}"
	fi	
}
dockerTag=get_docker_tag $TRAVIS_BRANCH $TRAVIS_COMMIT
echo started building docker image for $dockerTag

