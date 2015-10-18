/**
 * Created by 凯森 on 2015/3/4.
 */
var mongoose = require('mongoose')
var PostSchema=new mongoose.Schema({
    user_phone:String,
    user_name:String,
    user_image:String,
    time:String,  //发布日期
    content:String,
    image:[],
    song:String,
    comment_count:{type:Number,default:0},
    praise_count:{type:Number,default:0}
})
PostSchema.index({time: -1});
PostSchema.index({user_phone: 1, time: -1});
mongoose.model('Post', PostSchema);
