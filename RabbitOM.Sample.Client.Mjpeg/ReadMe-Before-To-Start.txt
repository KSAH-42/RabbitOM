//////////////////////////////////////////////////////////////////////////
                       ABOUT VLC
//////////////////////////////////////////////////////////////////////////

Recommendation if you are using VLC for testing mjpeg connection,
sometimes VLC will not be able to connect, because VLC rely in Live555, and Live555
doesn't not support some authorization scheme and algorithm
and sometimes VLC seems (not sure) to cache the previous connection settings
or to select the highest algorithm and finished to use basic if it's doesn't work

my recommendation is to change camera settings before, and then to close VLC, and then restart VLC.

For instance, if you are using vlc and hik camera will not be able to connect
if the configuration/system/security || configuration/système/Sécurité
on the web page as the following settings:
    "authentication RTSP   : digest"
and "RTSP Digest Algorithm : SHA256" 
and save settings, vlc will not be able to connect
instead, if you don't move forward test because vlc doesn't on mjpeg, try this sample even if vlc doesn't connect

//////////////////////////////////////////////////////////////////////////
                     ABOUT   HAPPY RTSP SERVER
//////////////////////////////////////////////////////////////////////////
This sample illustrate how to receive video from a Mjpeg source only. 

For H254, or H265, etc... others indivuals samples will be provided.

After configuring a jpeg rtsp server using the happyRtspServer, 

please make that you can receive the JPEG using VLC before launching this app

Download and configure this rtsp server:

https://www.happytimesoft.com/products/rtsp-server/index.html

go to the rtsp server folder, and add movies and the same folder and edit the xml configuration used by this server. and then launch this sample application.

you can also copy the configuration files located in Resources\Configuration\ folder include movies files that match with the configuration file

and then run using the following command: 

RtspServer.exe -c config-JPEG.xml

Otherwise use a security ip camera, and go the web page configuration section, and select the mjpeg codec.
and then read the manufacturer pdf in order to get the right rtsp uri. 
Please also, check the rtsp settings on the web page of the camera.

