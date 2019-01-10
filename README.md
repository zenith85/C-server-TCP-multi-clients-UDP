# C-server-TCP-multi-clients-UDP
This is how you can connect to server , then any clients sends a msg it will be broadcasted through the server to all clients

                                           ____________________________
                                          |                            |
                                          |         server             |
                                          |                            |
                                          ''''''''''''''''''''''''''''''
                                            |   ;             |   ;
                                            |   ;             |   ;
                                            |   ;             |   ;
                                            |   ;             |   ;
                                         tcp|   ;udp      tcp |   ;udp
                                            |   ;             |   ;
                                            |   ;             |   ;
                                            |   ;             |   ;
                                           ____________      ____________
                                          |            |    |            |
                                          |   client   |    |   client   |
                                          |            |    |            |
                                          ''''''''''''''    ''''''''''''''
                                          
                                       tcp does a socket connection, after that negotiate with the client
                                       to pick a udp port from a pool, thhis udp pool will be unique and un 
                                       ultered for future connection. each client will have a specific port for 
                                       connection.
                                       
This code is written by ibraheem raed altaha
