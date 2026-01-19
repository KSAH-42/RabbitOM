

This sample illustrate how to receive video from a Mjpeg source only. 

For H254, or H265, etc... others indivuals samples will be provided.

After configuring a jpeg rtsp server using the happyRtspServer, 

please make that you can receive the JPEG using VLC before launching this app

Download and configure this rtsp server:

https://www.happytimesoft.com/products/rtsp-server/index.html

go to the rtsp server folder, and add movies and the same folder and edit the xml configuration used by this server. and then launch this sample application.

you can also copy the configuration files located in Resources\Configuration\ folder

and run using the following command: 

RtspServer.exe -c config-JPEG.xml

Otherwise use a security ip camera, and go the web page configuration section, and select the mjpeg codec.
and then read the manufacturer pdf in order to get the right rtsp uri. 
Please also, check the rtsp settings on the web page of the camera.

