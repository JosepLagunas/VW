#!/bin/bash -e

REGISTRY_URL=${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com

SOURCE_IMAGE="${DOCKER_REPO}"

# using it as there will be 2 versions published
TARGET_IMAGE="${REGISTRY_URL}/${DOCKER_REPO}"
# lets make sure we always have access to latest image
TARGET_IMAGE_LATEST="${TARGET_IMAGE}:latest"

TAG_VERSION_IF_EXISTS=""

if [ "${TRAVIS_TAG}" != "" ]; then 
	echo "entra"
	TAG_VERSION_IF_EXISTS="-tag-${TRAVIS_TAG}"
fi

VERSION=${TRAVIS_BRANCH}-${TRAVIS_BUILD_NUMBER}-${TRAVIS_COMMIT}${TAG_VERSION_IF_EXISTS}

TARGET_IMAGE_VERSIONED="${TARGET_IMAGE}:${VERSION}"

aws configure set default.region ${AWS_REGION}

# Push image to ECR
###################

$(aws ecr get-login --no-include-email)

# update latest version
docker tag ${SOURCE_IMAGE} ${TARGET_IMAGE_LATEST}
docker push ${TARGET_IMAGE_LATEST}

# push new version
docker tag ${SOURCE_IMAGE} ${TARGET_IMAGE_VERSIONED}
docker push ${TARGET_IMAGE_VERSIONED}