package com.tfwcn.blog.controller;

import com.tfwcn.blog.dao.ErrorDao;
import com.tfwcn.blog.helper.CommonHelper;
import com.tfwcn.blog.models.ErrorInfo;
import com.tfwcn.blog.models.api.ResponseInfo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import java.util.Map;

/**
 * 错误操作类
 */
@RestController
@RequestMapping("/api/error")
public class ErrorController {
    @Autowired
    private ErrorDao errorDao;

    /**
     * 新增错误 /api/error/add
     *
     * @param reqMap 错误信息 message,detail
     * @return 返回状态码，1：成功
     */
    @RequestMapping(method = RequestMethod.POST, path = "/add")
    public ResponseEntity<?> add(@RequestBody Map<String, Object> reqMap) {
        try {
            //获取参数
            String message = (String) reqMap.get("message");
            String detail = (String) reqMap.get("detail");
            ErrorInfo errorInfo = new ErrorInfo();
            CommonHelper.getId(errorInfo);
            errorInfo.setMessage(message);
            errorInfo.setDetail(detail);
            errorDao.save(errorInfo);
            //返回值
            ResponseInfo responseInfo = new ResponseInfo(500, "错误代码：" + errorInfo.getNum());
            return ResponseEntity.ok(responseInfo);
        } catch (Exception ex) {
            //返回值
            ResponseInfo responseInfo = new ResponseInfo(500, ex.getMessage());
            return ResponseEntity.ok(responseInfo);
        }
    }
}
