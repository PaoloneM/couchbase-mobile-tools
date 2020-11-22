#!/bin/bash

echo Running large dataset generator

echo Going to create $RECORD_COUNT docs

dotnet LargeDatasetGenerator.dll --count $RECORD_COUNT -i $IF_PATH -o couchbase_server --url $CB_URL --username $CB_USER --password $CB_PASS --bucket $CB_BUCKET_NAME