/**
 * Created by 凯森 on 2015/3/4.
 */
var path = require('path');

var config = {
    // debug 为 true 时，用于本地调试
    debug: true,
    // cdn host，如 http://cnodejs.qiniudn.com
    site_static_host: '', // 静态文件存储域名
    // 社区的域名
    host: 'localhost',


    // mongodb 配置
    db: 'mongodb://kks:112699@localhost:27017/weibo',
    db_name: 'weibo',


    session_secret: 'kksweibo', // 务必修改
    auth_cookie_name: 'dreaming',

    // 程序运行的端口
    port: 8088,

    // 话题列表显示的话题数量
    list_topic_count: 20,

    // 限制发帖时间间隔，单位：毫秒
    post_interval: 2000,



    // 邮箱配置
    mail_opts: {
        host: 'smtp.126.com',
        port: 25,
        auth: {
            user: 'club@126.com',
            pass: 'club'
        }
    },



    // admin 可删除话题，编辑标签，设某人为达人
    admins: {user_login_name: true},



    //7牛的access信息，用于文件上传
    qn_access: {
        accessKey: 'your access key',
        secretKey: 'your secret key',
        bucket: 'your bucket name',
        domain: 'http://{bucket}.qiniudn.com'
    },

    //文件上传配置
    //注：如果填写 qn_access，则会上传到 7牛，以下配置无效
    upload: {
        path: path.join(__dirname, 'files/'),
        url: '/files/'
    },

    // 版块
    tabs: [
        ['share', '分享'],
        ['ask', '问答'],
        ['job', '招聘'],
    ],
    //微软推送
    wp_sid:'ms-app://s-1-15-2-2580729665-3516052597-3321334051-921133438-3719593538-163524723-4146671504',
    wp_client:'hbppU20n6IxRE3uE0s8szNoKQ04v9TV8',
    wp_token:''

};

module.exports = config;

