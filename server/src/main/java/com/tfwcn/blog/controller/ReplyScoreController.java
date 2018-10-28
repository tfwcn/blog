package com.tfwcn.blog.controller;

import com.tfwcn.blog.dao.RepliesScoreMapper;
import com.tfwcn.blog.helper.CommonHelper;
import com.tfwcn.blog.models.RepliesScore;
import com.tfwcn.blog.models.api.ResponseInfo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

/**
 * 回复评分记录 操作类
 */
@RestController
@RequestMapping("/api/reply_score")
public class ReplyScoreController {
    @Autowired
    private RepliesScoreMapper replyScoreDao;

    /**
     * 新增 回复评分记录 /api/reply_score/add
     *
     * @param replyScoreInfo 回复评分记录 id,num,userId,replyId,createTime,updateTime,
     * @return 返回状态码，1：成功
     */
    @RequestMapping(method = RequestMethod.POST, path = "/add")
    public ResponseEntity<?> add(@RequestBody RepliesScore replyScoreInfo) {
        try {
            CommonHelper.getId(replyScoreInfo);
            replyScoreDao.insert(replyScoreInfo);
            //返回值
            ResponseInfo responseInfo = new ResponseInfo(1, null);
            return ResponseEntity.ok(responseInfo);
        } catch (Exception ex) {
            //返回值
            ResponseInfo responseInfo = CommonHelper.SaveErrorLog(ex);
            return ResponseEntity.ok(responseInfo);
        }
    }
}
