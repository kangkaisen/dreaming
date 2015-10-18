/**
 * Created by 凯森 on 2015/3/3.
 */
var mongoose = require('mongoose')
var UserSchema = new mongoose.Schema({
    phone:String,
    name:{type:String,default:''},
    password:String,
    date: String,     //注册日期
    microsoft:String, //微软推送地址
    email:String,
    image:{type:String,default:''},
    dream:{type:String,default:''},
    fans_count:{type:Number,default:0}, //粉丝数目
    follow_count:{type:Number,default:0}, //关注数目
    post_count:{type:Number,default:0},
    score:{type: Number,default: 0 },
    uri:String,
    tags:[String]
})

UserSchema.index({phone: 1}, {unique: true});
mongoose.model('User', UserSchema);



