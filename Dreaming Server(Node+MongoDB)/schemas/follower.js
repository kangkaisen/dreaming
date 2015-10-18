/**
 * Created by 凯森 on 2015/3/5.
 */
var mongoose=require('mongoose');
var FollowSchema=new mongoose.Schema({
    cphone:String,
    cname:String,
    cimage:String,
    cdream:String,
    fphone:String,
    fname:String,
    fimage:String,
    fdream:String
})
FollowSchema.index({fphoen: 1,cphone:1});
FollowSchema.index({cphone:1});
mongoose.model('Follow', FollowSchema);