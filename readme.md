## SQL Server to Elasticsearch


This repository contains **dll** file for SQL Server.


### Installation

1. Install [Docker](https://www.docker.com/).

2. Download from public [Docker Hub Registry](https://registry.hub.docker.com/): `docker pull elasticsearch`

   
    ```
    docker pull elasticsearch
    docker run -d -p 9200:9200 -p 9300:9300 elasticsearch
    ```
3. Install CLR web client on SQL Server.


    ```SQL
    exec sp_configure 'clr enabled', 1;
    GO
    RECONFIGURE;
    GO

    ALTER DATABASE YOURDATABASE SET TRUSTWORTHY ON;
    GO

    CREATE ASSEMBLY SqlWebRequest
    FROM 'D:\...dll-dir-path..\SqlServerWebRequests.dll'
    WITH PERMISSION_SET=EXTERNAL_ACCESS;
    GO

    ```

4. ADD Function on SQL Server

    ```SQL
    CREATE FUNCTION dbo.fn_get_webrequest(
         @uri        nvarchar(max),
         @user       nvarchar(255)=NULL,
         @password     nvarchar(255)=NULL
    )
    RETURNS nvarchar(max)
    AS
    EXTERNAL NAME SqlWebRequest.Functions.GET;
    GO

    CREATE FUNCTION dbo.fn_post_webrequest(
         @uri         nvarchar(max),
         @postdata    nvarchar(max),
         @user        nvarchar(255)=NULL,
         @password    nvarchar(255)=NULL
    )
    RETURNS nvarchar(max)
    AS
        EXTERNAL NAME SqlWebRequest.Functions.POST;
    GO
    ```

5. Test SQL server to Elasticsearch.

    ```sql
    PRINT dbo.fn_get_webrequest('http://127.0.01:9200', NULL, NULL);
    {
      "name" : "0f4rETL",
      "cluster_name" : "elasticsearch",
      "cluster_uuid" : "aCURZZGCRaqov1vN5_Nhzg",
      "version" : {
        "number" : "5.4.0",
        "build_hash" : "780f8c4",
        "build_date" : "2017-04-28T17:43:27.229Z",
        "build_snapshot" : false,
        "lucene_version" : "6.5.0"
      },
      "tagline" : "You Know, for Search"
    }
    ```


### Usage

1. Add Sample Document.

    ```sql
    SELECT dbo.fn_post_webrequest(
        'http://127.0.01:9200/twitter/tweet/'
        , '{
            "user" : "kimchy",
            "post_date" : "2009-11-15T14:12:12",
            "message" : "trying out Elasticsearch"
        }'
        , NULL
        , NULL);
    ```


    result
    ```json
    {
       "_index":"twitter",
       "_type":"tweet",
       "_id":"AVyefxjEnVKf5Cd8sVNX",
       "_version":1,
       "result":"created",
       "_shards":{
          "total":2,
          "successful":1,
          "failed":0
       },
       "created":true
    }
    ```

2. Get SQL Result for Elasticsearch

    ```sql
    SELECT dbo.fn_get_webrequest(
        'http://127.0.01:9200/twitter/tweet/_search?q=user:kimchy&pretty'
        , NULL
        , NULL);
    ```
    result
    ```json
    {
      "took" : 80,
      "timed_out" : false,
      "_shards" : {
        "total" : 5,
        "successful" : 5,
        "failed" : 0
      },
      "hits" : {
        "total" : 1,
        "max_score" : 0.2876821,
        "hits" : [
          {
            "_index" : "twitter",
            "_type" : "tweet",
            "_id" : "AVyeih8oXH2SYJrJes9d",
            "_score" : 0.2876821,
            "_source" : {
              "user" : "kimchy",
              "post_date" : "2009-11-15T14:12:12",
              "message" : "trying out Elasticsearch"
            }
          }
        ]
      }
    }
   ```

#### Elasticsearch Result handle on SQL Server


```
```
