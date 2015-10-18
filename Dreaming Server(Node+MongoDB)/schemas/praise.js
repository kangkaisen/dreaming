/**
 * Created by 凯森 on 2015/3/7.
 */
var mongoose=require('mongoose');
var Schema = mongoose.Schema;
var ObjectId = Schema.ObjectId;
var PraiseSchema=new Schema({
    post_id:{type: ObjectId},
    user_phone:{type:String}
})
PraiseSchema.index({post_id: 1,user_phone:1});

mongoose.model('Praise', PraiseSchema);