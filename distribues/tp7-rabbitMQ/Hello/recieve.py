#!/usr/bin/env python
import pika

def callback(ch, method, properties, body):
    print(" [x] Received %r" % (body,))
	
connection = pika.BlockingConnection(pika.ConnectionParameters(host='localhost'))
print(connection)
channel = connection.channel()
print(channel)
channel.queue_declare(queue='hello')

print(' [*] Waiting for messages. To exit press CTRL+C')

channel.basic_consume(callback, queue='hello', no_ack=True)

channel.start_consuming()
