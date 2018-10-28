package com.tfwcn.blog.controller;

import com.tfwcn.blog.dao.NotesTypeMapper;
import com.tfwcn.blog.helper.CommonHelper;
import com.tfwcn.blog.models.NotesType;
import com.tfwcn.blog.models.api.ResponseInfo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.Collection;

/**
 * 贴子类型 操作类
 */
@RestController
@RequestMapping("/api/note_type")
public class NoteTypeController {
    @Autowired
    private NotesTypeMapper noteTypeDao;

    /**
     * 新增 贴子类型 /api/note_type/add
     *
     * @param noteTypeInfo 贴子类型 id,num,name,path,detail,createTime,updateTime,
     * @return 返回状态码，0：成功
     */
    @RequestMapping(method = RequestMethod.POST, path = "/add")
    public ResponseEntity<?> add(@RequestBody NotesType noteTypeInfo) {
        try {
            CommonHelper.getId(noteTypeInfo);
            noteTypeDao.insert(noteTypeInfo);
            //返回值
            ResponseInfo responseInfo = new ResponseInfo(1, null);
            return ResponseEntity.ok(responseInfo);
        } catch (Exception ex) {
            //返回值
            ResponseInfo responseInfo = CommonHelper.SaveErrorLog(ex);
            return ResponseEntity.ok(responseInfo);
        }
    }

    /**
     * 新增 贴子类型 /api/note_type/get_list
     *
     * @param id 贴子类型 id
     * @return 返回状态码，0：成功
     */
    @RequestMapping(method = RequestMethod.GET, path = "/get_list")
    public ResponseEntity<?> getList(@RequestParam(required = false) String id) {
        try {
            Collection<NotesType> tmpListNoteTypeInfo = noteTypeDao.selectAll();
            //返回值
            ResponseInfo responseInfo = new ResponseInfo(1, tmpListNoteTypeInfo);
            return ResponseEntity.ok(responseInfo);
        } catch (Exception ex) {
            //返回值
            ResponseInfo responseInfo = CommonHelper.SaveErrorLog(ex);
            return ResponseEntity.ok(responseInfo);
        }
    }
}
