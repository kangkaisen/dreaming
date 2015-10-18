/**
 * Created by 凯森 on 2015/3/7.
 */

var Comment=require('../modles').Comment;
var Post=require('../modles').Post;

exports.newComment=function(req,res){

    Comment.newAndSave(req.body.id,req.body.phone,req.body.image,req.body.name,req.body.content,req.body.time,req.body.atName,req.body.atPhone,function(err,comment){
        res.sendStatus(200);
        Post.updateCommentCount(req.body.id,function(err,post){

        })
    })
}

exports.getComments=function(req,res){
    Comment.getByPostId(req.params.id,function(err,comments){
        res.json(comments);
    })
}