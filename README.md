# Navigating the Stream - Getting started with Kafka in .NET

## Introduction

Kafka is the de-facto standard when it comes to building real-time data pipelines. 
It is a distributed, horizontally scalable, fault-tolerant, commit log. 
It is designed to allow a single cluster to serve as the central data backbone 
for a large organization. It can be elastically and transparently expanded without downtime. 
Data streams are partitioned and spread over a cluster of machines to allow data 
streams larger than the capability of any single machine and to allow clusters of 
co-ordinated consumers.

## Run a local Kafka environment

In this repository there's a docker-compose file that will enable you to start a local
kafka environment with a single command. The environment consists of:
- A single kafka broker, running [bitnami-kafka](https://hub.docker.com/r/bitnami/kafka) accessible on `localhost:9094` using the credentials "kafka:mysecretpassword"
- [Kafka-UI](https://github.com/kafbat/kafka-ui) for monitoring the kafka topics, accessible on `http://localhost:8080/`
- [Karapace Schema Registry](https://github.com/Aiven-Open/karapace), accessible on `http://localhost:8081/`

If you've got docker up and running locally, then all you need to do is run the following command:
```bash
docker compose up
``` 

## Kafka in .NET

There are a few libraries available for working with Kafka in .NET. For this demonstration we will 
be using [Chr.Avro.Confluent](https://www.nuget.org/packages/Chr.Avro.Confluent/) which is a .NET library 
simplifying access to Kafka, especially when serializing and deserializing Apache Avro payloads. 
It is built on top of the Confluent.Kafka library but in our opinion it is easier to use and in some cases 
it can deliver better performance when deserializing avro data.

## The examples

In this repository you will find four very basic examples to get you started with Kafka in .NET.

1. **KafkaProducer**: A simple producer that sends a string-message to a Kafka topic.
2. **KafkaConsumer**: A simple consumer that reads string-messages from a Kafka topic.
3. **KafkaSchemaProducer**: A simple producer that sends a message to a Kafka topic using Avro serialization.
4. **KafkaSchemaConsumer**: A simple consumer that reads messages from a Kafka topic using Avro deserialization.
