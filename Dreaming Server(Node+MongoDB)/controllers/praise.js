/**
 * Created by 凯森 on 2015/3/7.
 */
var Praise = require('../modles').Praise;
var Post=require('../modles').Post;

exports.newPraise=function(req,res){
    Praise.findPraise(req.params.id,req.params.phone,function(err,praise){
        if(!praise){
            Praise.newAndSave(req.params.id,req.params.phone,function(err,praise){
                res.sendStatus(200);
                Post.updatePraiseCount(req.params.id,function(err,post){

                })
            })
        }
        else{
            res.sendStatus(200);
        }
    })
}