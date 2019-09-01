package com.tfwcn.blog.controller;

import com.github.pagehelper.PageHelper;
import com.github.pagehelper.PageInfo;
import com.tfwcn.blog.dao.NotesMapper;
import com.tfwcn.blog.helper.CommonHelper;
import com.tfwcn.blog.models.Notes;
import com.tfwcn.blog.models.api.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import java.util.Date;
import java.util.List;

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
     * @param request 贴子信息 title,content,typeId,
     * @return 返回状态码，1：成功
     */
    @RequestMapping(method = RequestMethod.POST, path = "/add")
    public ResponseEntity<?> add(@RequestBody NotesAddRequest request) {
        try {
            var model=new Notes();
            model.setTitle(request.getTitle());
            model.setContent(request.getContent());
            model.setTypeId(request.getTypeId());
            model.setState(0);
            CommonHelper.getId(model);
            noteDao.insert(model);
            //返回值
            var responseInfo = new ResponseInfo(0, null);
            return ResponseEntity.ok(responseInfo);
        } catch (Exception ex) {
            //返回值
            ResponseInfo responseInfo = CommonHelper.SaveErrorLog(ex);
            return ResponseEntity.ok(responseInfo);
        }
    }

    /**
     * 新增 贴子信息 /api/note/edit
     *
     * @param request 贴子信息 id,title,content,typeId,
     * @return 返回状态码，1：成功
     */
    @RequestMapping(method = RequestMethod.POST, path = "/edit")
    public ResponseEntity<?> edit(@RequestBody NotesEditRequest request) {
        try {
            var model=noteDao.selectByPrimaryKey(request.getId());
            if(model==null)
                throw new Exception("记录不存在");
            model.setTitle(request.getTitle());
            model.setContent(request.getContent());
            model.setTypeId(request.getTypeId());
            model.setUpdateTime(new Date());
            noteDao.updateByPrimaryKey(model);
            //返回值
            var responseInfo = new ResponseInfo(0, null);
            return ResponseEntity.ok(responseInfo);
        } catch (Exception ex) {
            //返回值
            ResponseInfo responseInfo = CommonHelper.SaveErrorLog(ex);
            return ResponseEntity.ok(responseInfo);
        }
    }

    /**
     * 查询 贴子信息 /api/note/list
     *
     * @param request 查询条件 page,rows,
     * @return 返回状态码，1：成功
     */
    @RequestMapping(method = RequestMethod.POST, path = "/list")
    public ResponseEntity<?> list(@RequestBody NotesListRequest request) {
        try {
            //获取第1页，10条内容，默认查询总数count
            PageHelper.startPage(request.getPage(), request.getRows());
            var list = noteDao.selectAll(1);
            //用PageInfo对结果进行包装
            PageInfo page = new PageInfo(list);
            //返回值
            var responseInfo = new ResponseInfo<>(0, page);
            return ResponseEntity.ok(responseInfo);
        } catch (Exception ex) {
            //返回值
            ResponseInfo responseInfo = CommonHelper.SaveErrorLog(ex);
            return ResponseEntity.ok(responseInfo);
        }
    }

    /**
     * 查询 贴子信息 /api/note/model
     *
     * @param request 查询条件 page,rows,
     * @return 返回状态码，1：成功
     */
    @RequestMapping(method = RequestMethod.POST, path = "/model")
    public ResponseEntity<?> model(@RequestBody NotesModelRequest request) {
        try {
            //通过序号获取记录
            var model = noteDao.selectByNum(request.getNum());
            //返回值
            var responseInfo = new ResponseInfo<>(0, model);
            return ResponseEntity.ok(responseInfo);
        } catch (Exception ex) {
            //返回值
            ResponseInfo responseInfo = CommonHelper.SaveErrorLog(ex);
            return ResponseEntity.ok(responseInfo);
        }
    }
}
