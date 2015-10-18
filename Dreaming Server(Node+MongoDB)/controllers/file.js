/**
 * Created by 凯森 on 2015/3/10.
 */

var path = require('path');
//接受录音文件
exports.receiveSong=function(req,res){
    var songpath=path.basename(req.files.song.path);
    res.write(songpath);
    res.end()
}

//接受照片文件
exports.receivePictrue=function(req,res){
    var imagepath=path.basename(req.files.pic.path);
    res.write(imagepath);
    res.end()
}