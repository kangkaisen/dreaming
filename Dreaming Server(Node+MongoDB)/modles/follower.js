var modles = require('../schemas');
var Follow = modles.Follow;


exports.newAndSave=function(cphone,cname,cimage,cdream,fphone,fname,fimage,fdream,callback){
    var follow=new Follow();
    follow.cphone=cphone;
    follow.cname=cname;
    follow.cimage=cimage;
    follow.cdream=cdream;
    follow.fphone=fphone;
    follow.fname=fname;
    follow.fimage=fimage;
    follow.fdream=fdream;
    follow.save(callback);
}
//获取关注者
exports.getCare=function(fphone,callback){
    Follow.find({fphone:fphone},null,{limit:1000},callback);
}
//获取粉丝
exports.getFollow=function(cphone,callback){
    Follow.find({cphone:cphone},null,{limit:1000},callback);
}
//取消关注
exports.unFollow=function(fphone,cphone,callback){
    Follow.remove({fphone:fphone,cphone:cphone},callback);
}
//获取唯一follower
exports.getOneFollow=function(fphone,cphone,callback){
    Follow.findOne({fphone:fphone,cphone:cphone},callback);
}