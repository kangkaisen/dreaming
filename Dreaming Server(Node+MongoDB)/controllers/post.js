/**
 * Created by 凯森 on 2015/3/4.
 */
var path = require('path');
var Post = require('../modles').Post;
var User=require('../modles').User;

exports.publish=function(req,res){
    var imageList=[];
    if(req.files.pic instanceof Array){
        for(var i=0;i<req.files.pic.length;i++){
            var l={
                i:path.basename(req.files.pic[i].path)
            };
            imageList.push(l);
        }
    }
    else if(req.files.pic)
    {
        var l={ i:path.basename(req.files.pic.path)};
        imageList.push(l);
    }
    if(req.files.song!=null){
        var songpath=path.basename(req.files.song.path);
    }


    Post.newAndSave(req.body.phone,req.body.name,req.body.uimage,req.body.content,imageList,songpath,req.body.time,function(err,post){
        if(err){
            return res.json({msg:err});
        }
        return res.json(post);


    });
    User.incPostCount(req.body.phone,function(err,user){

    })

}

exports.getAll=function(req,res){
    Post.getAllPost(function (err,posts) {

        if(posts){
            return res.json(posts);
        }
        else{
            return res.sendStatus(404);
        }
    })
}

exports.getUserAll=function(req,res){
    Post.getUserPost(req.params.phone,function(err,posts){
        if(posts){
            return res.json(posts);
        }
        else{
            return res.sendStatus(404);
        }
    })
}