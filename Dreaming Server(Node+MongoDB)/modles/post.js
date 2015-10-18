/**
 * Created by 凯森 on 2015/3/3.
 */

var models = require('../schemas');
var Post = models.Post;

exports.getAllPost=function(callback){
    Post.find({},null,{sort:{time: -1},limit:500},callback);
}

exports.getUserPost=function(user_phone,callback){
    Post.find({user_phone:user_phone},null,{sort:{time: -1},limit:500},callback);
}
/**
 * 更新Post的comment_count
 * @param {String} topicId 主题ID
 * @param {String} replyId 回复ID
 * @param {Function} callback 回调函数
 */
exports.updateCommentCount = function (Id, callback) {
    Post.findOne({_id: Id}, function (err, post) {
        if (err || !post) {
            return callback(err);
        }

        post.comment_count += 1;
        post.save(callback);
    });
};

exports.updatePraiseCount=function(id,callback){
    Post.findOne({_id:id}, function (err,post){
            if (err || !post) {
                return callback(err);
            }

            post.praise_count += 1;
            post.save(callback);
        })
}

//根据id获取Post
exports.getPost = function (id, callback) {
    Post.findOne({_id: id}, callback);
};

//保存Post
exports.newAndSave = function (user_phone,user_name,user_image,content,image,song,time,callback) {
    var post = new Post();
    post.user_phone=user_phone;
    post.user_name=user_name;
    post.user_image=user_image;
    post.content=content;
    post.image=image;
    post.song=song;
    post.time=time;
    post.save(callback);
};

