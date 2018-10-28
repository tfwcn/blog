package com.tfwcn.blog.controller;

import com.tfwcn.blog.dao.RepliesMapper;
import com.tfwcn.blog.helper.CommonHelper;
import com.tfwcn.blog.models.Replies;
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
    private RepliesMapper replyDao;

    /**
     * 新增 回复信息 /api/reply/add
     *
     * @param replyInfo 回复信息 id,num,userId,noteId,replyId,content,score,createTime,updateTime,
     * @return 返回状态码，1：成功
     */
    @RequestMapping(method = RequestMethod.POST, path = "/add")
    public ResponseEntity<?> add(@RequestBody Replies replyInfo) {
        try {
            CommonHelper.getId(replyInfo);
            replyDao.insert(replyInfo);
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
