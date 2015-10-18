/**
 * Created by 凯森 on 2015/3/5.
 */
var Follow = require('../modles').Follow;
var User=require('../modles').User;

exports.newFollow=function(req,res){
    Follow.getOneFollow(req.body.fphone,req.body.cphone,function(err,follow){
        if(!follow){

            Follow.newAndSave(req.body.cphone,req.body.cname,req.body.cimage,req.body.cdream,req.body.fphone,req.body.fname,req.body.fimage,req.body.fdream,function(err,follow){

                 res.sendStatus(200);



            })
            User.incCareCount(req.body.fphone,function(err,follow){

            })
            User.incFollowCount(req.body.cphone,function(err,follow){

            })
        }
        else{
            res.sendStatus(200);
        }

    })

}

exports.un=function(req,res){
    Follow.getOneFollow(req.params.fphone,req.params.cphone,function(err,follow){
        if(follow){
            Follow.unFollow(req.params.fphone,req.params.cphone,function(err,follow){
                res.sendStatus(200);
                User.decCareCount(req.params.fphone,function(err,follow){

                })
                User.decFollowCount(req.params.cphone,function(err,follow){

                })
            })
        }
        else{
            res.sendStatus(200);
        }
    })

}

exports.getCares=function(req,res){
    Follow.getCare(req.params.fphone,function(err,followers){
        if(followers){
            res.json(followers);
        }
        else{
            res.sendStatus(200);
        }
    })
}

exports.getFollows=function(req,res){
    Follow.getFollow(req.params.cphone,function(err,followers){
        if(followers){
            res.json(followers);
        }
        else{
            res.sendStatus(200);
        }
    })
}
