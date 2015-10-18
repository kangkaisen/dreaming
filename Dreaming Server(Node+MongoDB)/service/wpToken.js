/**
 * Created by 凯森 on 2015/3/9.
 */
var https=require ('https');
var config=require('../config');
var querystring = require('querystring');
var post_data = querystring.stringify({
    grant_type:"client_credentials",
    client_id:config.wp_sid,
    client_secret:config.wp_client,
    scope:"notify.windows.com"
});

var options={
    host:'login.live.com',
    port:'443',
    method:'post',
    path:'/accesstoken.srf',
    headers: {
        'Content-Type': 'application/x-www-form-urlencoded',
        'Content-Length': post_data.length
    }
};
var time;
function httpsPost(){



    var reqHttps = https.request(options, function(resHttps) {
        if(resHttps.statusCode==200){
            resHttps.on('data', function(body1) {
                var body=JSON.parse(body1);
                config.wp_token=body.access_token;

            });
        }




    });


    reqHttps.write(post_data);
    reqHttps.end();


}
module.exports=function wpGetToken(){
    httpsPost();
    setInterval(httpsPost,80000000);

}

