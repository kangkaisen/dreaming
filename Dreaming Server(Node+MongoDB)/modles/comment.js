var models=require('../schemas');
var Comment=models.Comment;

//保存评论
exports.newAndSave=function(post_id,user_phone,user_image,user_name,content,time,at_name,at_phone,callback){
    var comment=new Comment();
    comment.post_id=post_id;
    comment.user_phone=user_phone;
    comment.user_image=user_image;
    comment.user_name=user_name;
    comment.content=content;
    comment.time=time;
    comment.at_name=at_name;
    comment.at_phone=at_phone;
    comment.save(callback);
}

//根据post_id获取评论

exports.getByPostId=function(post_id,callback){
    Comment.find({post_id:post_id},null,{sort:{time:-1}},callback);
}