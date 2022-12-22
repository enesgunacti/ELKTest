# ELK 

In this project i tried to implement ELK on C# Rest API Using `Nest` package

Firstly, i don't have any idea about the ELK and i started to search this technologies. Then in the project i tried run ELK with `localhost` beacuse i thought next step is using ELK in online.

See [Elastic Search Documentation](https://www.elastic.co/guide/index.html).

After reading some documentation. I need to work with `Elastic Search` and i found some videos on YouTube. When i looked these videos they are always working `ELK` in local with C# Rest API.

I Already have some projects on my GitHub account C# API . I didn't have much trouble API part.

## ELK Configuration

For using ELK in your computer you need to start this technology localy. 

Kibana

Kibana is free and open frontend application for testing your Query for the `ELK` you can think like Postman or Swagger.

1) `ELK and Kibana Installation`

    [Download Kibana and ELK](https://www.elastic.co/downloads/).
    
    Use terminal or cmd for the starting applications
    
    We have two steps
    
    1) CMD

    For ELK:
    ```sh
    cd elasticsearch-7.16.1-windows-x86_64\elasticsearch-7.16.1\bin
        elasticsearch-7.16.1-windows-x86_64\elasticsearch-7.16.1\bin>elasticsearch.bat
    ```

    For Kibana:
      ```sh
        cd elasticsearch-7.16.1-windows-x86_64\elasticsearch-7.16.1\bin
            elasticsearch-7.16.1-windows-x86_64\elasticsearch-7.16.1\bin>elasticsearch.bat
    ```  
    After installations you will take Info For Kibana and you can check your Elactisearch application on "localhost:9200" , If you take  `Kibana is now avaliable` check "localhost:5601 or 5602"

If you installed correctly you can write your application on C# know. Because I had many problem for the installation  `ELK & Kibana`
