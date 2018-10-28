package com.tfwcn.blog.controller;

import com.tfwcn.blog.dao.NotesMapper;
import com.tfwcn.blog.helper.CommonHelper;
import com.tfwcn.blog.models.Notes;
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
    private NotesMapper noteDao;

    /**
     * 新增 贴子信息 /api/note/add
     *
     * @param noteInfo 贴子信息 id,num,title,content,typeId,createTime,updateTime,
     * @return 返回状态码，1：成功
     */
    @RequestMapping(method = RequestMethod.POST, path = "/add")
    public ResponseEntity<?> add(@RequestBody Notes noteInfo) {
        try {
            CommonHelper.getId(noteInfo);
            noteDao.insert(noteInfo);
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
