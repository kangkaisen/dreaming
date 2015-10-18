/**
 * Created by 凯森 on 2015/3/3.
 */

var modles = require('../schemas');
var User = modles.User;

/**
 * 根据手机号查找用户
 * Callback:
 * - err, 数据库异常
 * - user, 用户
 * @param {String} phone 手机号
 * @param {Function} callback 回调函数
 */
exports.getUserByPhone = function (phone, callback) {
    User.findOne({'phone': phone}, callback);
};


/**
 * 根据用户ID，查找用户
 * Callback:
 * - err, 数据库异常
 * - user, 用户
 * @param {String} id 用户ID
 * @param {Function} callback 回调函数
 */
exports.getUserById = function (id, callback) {
    User.findOne({_id: id}, callback);
};
/**
 * 用户注册
 */

exports.newAndSave = function (phone,password,date,callback) {
    var user = new User();
    user.phone = phone;
    user.password=password;
    user.date=date;
    user.save(callback);
};
//更新用户资料
exports.update=function(phone,name,dream,image,callback){
    User.findOne({phone:phone},function(err,user){
        if(err||!user){
            return callback(err);
        }
        user.name=name;
        user.image=image;
        user.dream=dream;
        user.save(callback);

    })
}

//增加用户dreaming数
exports.incPostCount=function(phone,callback){
    User.findOne({phone:phone},function(err,user){
        if(err||!user){
            return callback(err);
        }
        user.post_count+=1;
        user.save(callback);
    })
}
//增加用户关注者数
exports.incCareCount=function(fphone,callback){
    User.findOne({phone:fphone},function(err,user){
        if(err||!user){
            return callback(err);
        }
        user.follow_count+=1;
        user.save(callback);
    })
}
//增加用户粉丝数
exports.incFollowCount=function(cphone,callback){
    User.findOne({phone:cphone},function(err,user){
        if(err||!user){
            return callback(err);
        }
        user.fans_count+=1;
        user.save(callback);
    })
}
//减少用户关注者数
exports.decCareCount=function(fphone,callback){
    User.findOne({phone:fphone},function(err,user){
        if(err||!user){
            return callback(err);
        }
        user.follow_count-=1;
        user.save(callback);
    })
}

//减少用户粉丝数
exports.decFollowCount=function(cphone,callback){
    User.findOne({phone:cphone},function(err,user){
        if(err||!user){
            return callback(err);
        }
        user.fans_count-=1;
        user.save(callback);
    })
}

//更新微软推送地址
exports.urlUpdate=function(phone,url,callback){
    User.update({phone:phone},{$set : { microsoft: url }},callback);
}