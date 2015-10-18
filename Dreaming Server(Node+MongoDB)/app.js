/**
 * Created by 凯森 on 2015/3/3.
 */
var config = require('./config');
var path = require('path');
var routes = require('./routes');
var bodyParser = require('body-parser');
var errorhandler = require('errorhandler');
var multer = require('multer');
var morgan = require('morgan');
var chat = require('./service/chat.js');
var wpGetToken=require('./service/wpToken.js');
var express = require('express');


var app = express();
var server = require('http').Server(app);

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({
    extended: true
}));
app.use(multer({
    dest: './files/',
    limits: {
        fileSize: 100000

    }
}))
app.use('/files', express.static(__dirname + '/files'));
app.use(require('method-override')());

app.use(require('response-time')());


app.use(morgan('dev'));
app.use(errorhandler());
// routes
app.use('/', routes);






server.listen(config.port, function () {
    console.log("dreaming listening on port %d in %s mode", config.port, app.settings.env);


});
chat(server);
wpGetToken();
module.exports = app;