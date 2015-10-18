/**
 * Created by 凯森 on 2015/3/9.
 */
var https = require('https');
var config = require('../config');

module.exports = function wpPush(path, data) {

    var post_data = new Buffer(data);
    var options = {
        host: 'sin.notify.windows.com',
        port: '443',
        method: 'post',
        path: path,
        headers: {
            'Content-Type': 'application/octet-stream',
            'Content-Length': post_data.length,
            'Authorization': 'Bearer ' + config.wp_token,
            'X-WNS-Type': 'wns/raw'
        }
    };


    var reqHttps = https.request(options, function (resHttps) {
        resHttps.on('data', function (body1) {
            console.log("body:" + body1);
        });
    });
    reqHttps.write(post_data);
    reqHttps.end();


}
