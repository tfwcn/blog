package com.tfwcn.blog.controller;

import com.tfwcn.blog.dao.ErrorDao;
import com.tfwcn.blog.dao.NoteDao;
import com.tfwcn.blog.helper.CommonHelper;
import com.tfwcn.blog.models.ErrorInfo;
import com.tfwcn.blog.models.NoteInfo;
import com.tfwcn.blog.models.api.ResponseInfo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

/**
 * 贴子信息 操作类
 */
@RestController
@RequestMapping("/api/note")
public class NoteController {
    @Autowired
    private NoteDao noteDao;
    @Autowired
    private ErrorDao errorDao;

    /**
     * 新增 贴子信息 /api/note/add
     *
     * @param noteInfo 贴子信息 id,num,title,content,typeId,createTime,updateTime,
     * @return 返回状态码，1：成功
     */
    @RequestMapping(method = RequestMethod.POST, path = "/add")
    public ResponseEntity<?> add(@RequestBody NoteInfo noteInfo) {
        try {
            CommonHelper.getId(noteInfo);
            noteDao.save(noteInfo);
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
