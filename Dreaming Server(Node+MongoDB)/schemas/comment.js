/**
 * Created by 凯森 on 2015/3/7.
 */
var mongoose=require('mongoose');
var Schema = mongoose.Schema;
var ObjectId = Schema.ObjectId;

var CommentSchema=new Schema({
    post_id:{type: ObjectId},
    user_phone:{type:String},
    user_image:{type:String},
    user_name:{type:String},
    content:{type:String},
    time:{type:String},  //时间
    at_name:{type:String,default:''},
    at_phone:{type:String,default:''}//@用户
})
CommentSchema.index({post_id: 1,time:-1});

mongoose.model('Comment', CommentSchema);