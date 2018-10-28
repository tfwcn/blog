package com.tfwcn.blog.controller;

import com.tfwcn.blog.dao.ErrorDao;
import com.tfwcn.blog.dao.ReplyDao;
import com.tfwcn.blog.helper.CommonHelper;
import com.tfwcn.blog.models.ErrorInfo;
import com.tfwcn.blog.models.ReplyInfo;
import com.tfwcn.blog.models.api.ResponseInfo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

/**
 * 回复信息 操作类
 */
@RestController
@RequestMapping("/api/reply")
public class ReplyController {
    @Autowired
    private ReplyDao replyDao;
    @Autowired
    private ErrorDao errorDao;

    /**
     * 新增 回复信息 /api/reply/add
     *
     * @param replyInfo 回复信息 id,num,userId,noteId,replyId,content,score,createTime,updateTime,
     * @return 返回状态码，1：成功
     */
    @RequestMapping(method = RequestMethod.POST, path = "/add")
    public ResponseEntity<?> add(@RequestBody ReplyInfo replyInfo) {
        try {
            CommonHelper.getId(replyInfo);
            replyDao.save(replyInfo);
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
