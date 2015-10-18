/**
 * Created by 凯森 on 2015/3/4.
 */
var mongoose = require('mongoose')
var config = require('../config')

mongoose.connect(config.db, function (err) {
    if (err) {
        console.error('connect to %s error: ', config.db_name, err.message)
        process.exit(1)
    }
})

// models
require('./user')
require('./post')
require('./follower')
require('./praise')
require('./comment')
exports.User = mongoose.model('User');
exports.Post = mongoose.model('Post');
exports.Follow=mongoose.model('Follow');
exports.Praise=mongoose.model('Praise');
exports.Comment=mongoose.model('Comment');
