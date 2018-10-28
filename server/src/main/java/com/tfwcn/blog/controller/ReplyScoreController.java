package com.tfwcn.blog.controller;

import com.tfwcn.blog.dao.ErrorDao;
import com.tfwcn.blog.dao.ReplyScoreDao;
import com.tfwcn.blog.helper.CommonHelper;
import com.tfwcn.blog.models.ErrorInfo;
import com.tfwcn.blog.models.ReplyScoreInfo;
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
    private ReplyScoreDao replyScoreDao;
    @Autowired
    private ErrorDao errorDao;

    /**
     * 新增 回复评分记录 /api/reply_score/add
     *
     * @param replyScoreInfo 回复评分记录 id,num,userId,replyId,createTime,updateTime,
     * @return 返回状态码，1：成功
     */
    @RequestMapping(method = RequestMethod.POST, path = "/add")
    public ResponseEntity<?> add(@RequestBody ReplyScoreInfo replyScoreInfo) {
        try {
            CommonHelper.getId(replyScoreInfo);
            replyScoreDao.save(replyScoreInfo);
            //返回值
            ResponseInfo responseInfo = new ResponseInfo(1, null);
            return ResponseEntity.ok(responseInfo);
        } catch (Exception ex) {
            //记录错误
            ErrorInfo errorInfo = new ErrorInfo();
            CommonHelper.getId(errorInfo);
            errorInfo.setMessage(ex.getMessage());
            errorInfo.setDetail(CommonHelper.getExceptionDetail(ex));
            errorDao.save(errorInfo);
            //返回值
            ResponseInfo responseInfo = new ResponseInfo(500, "错误代码：" + errorInfo.getNum());
            return ResponseEntity.ok(responseInfo);
        }
    }
}
