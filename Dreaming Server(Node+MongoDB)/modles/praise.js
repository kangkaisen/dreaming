/**
 * Created by 凯森 on 2015/3/7.
 */
var models = require('../schemas');
var Praise = models.Praise;

exports.newAndSave=function(post_id,user_phone,callback){
    var praise=new Praise();
    praise.post_id=post_id;
    praise.user_phone=user_phone;
    praise.save(callback);
}

exports.findPraise=function(post_id,user_phone,callback){
    Praise.findOne({post_id:post_id,user_phone:user_phone},callback);
}