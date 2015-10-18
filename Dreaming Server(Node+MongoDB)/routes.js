var express = require('express');
var user = require('./controllers/user');
var post=require('./controllers/post');
var follow=require('./controllers/follower');
var praise=require('./controllers/praise');
var comment=require('./controllers/comment');
var file=require('./controllers/file');


var router = express.Router();

router.post('/api/user/signin',user.signin);// 用户注册
router.post('/api/user/login',user.login) ;  //用户登录
router.post('/api/user/update',user.update);//用户更新资料
router.get('/api/user/:phone',user.info);   //用户信息
router.post('/api/user/url',user.updateUrl); //更新用户推送地址

router.post('/api/dreaming/publish',post.publish); //发布梦想
router.get('/api/dreaming',post.getAll);          //获取所有dreaming
router.get('/api/dreaming/:phone',post.getUserAll); //获取指定用户dreaming

router.post('/api/user/follow',follow.newFollow); //新增关注
router.get('/api/user/unfollow/:fphone/:cphone',follow.un);//取消关注
router.get('/api/user/carers/:fphone',follow.getCares);  //获取所有关注者
router.get('/api/user/followers/:cphone',follow.getFollows); //获取所有粉丝

router.get('/api/dreaming/praise/:id/:phone',praise.newPraise); //点赞

router.post('/api/comment/publish',comment.newComment); //发表评论
router.get('/api/comment/:id',comment.getComments);  //获取post的所有评论

router.post('/api/chat/record',file.receiveSong); //接受录音文件
router.post('/api/chat/image',file.receivePictrue); //接受照片文件

module.exports = router;

