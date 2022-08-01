#!/usr/bin/env bash

GOOGLE_JSON_FILE=$APPCENTER_SOURCE_DIRECTORY/ExpenseTrackerApp/ExpenseTrackerApp.Droid/google-services.json

if [ -e "$GOOGLE_JSON_FILE" ]  
then  
    echo "Atualizando Google Json com a variavel de ambiente do appcenter"
    echo "$GOOGLE_JSON" > $GOOGLE_JSON_FILE
    sed -i -e 's/\\"/'\"'/g' $GOOGLE_JSON_FILE

    echo "Conteudo para o arquivo google-services.json escrito:"
    cat $GOOGLE_JSON_FILE
fi  