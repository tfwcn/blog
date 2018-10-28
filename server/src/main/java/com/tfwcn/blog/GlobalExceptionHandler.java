package com.tfwcn.blog;

import com.tfwcn.blog.dao.ErrorDao;
import com.tfwcn.blog.helper.CommonHelper;
import com.tfwcn.blog.models.ErrorInfo;
import com.tfwcn.blog.models.api.ResponseInfo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.ResponseBody;

import javax.servlet.http.HttpServletRequest;

/**
 * 全局异常处理
 */
@ControllerAdvice
public class GlobalExceptionHandler {
    @Autowired
    private ErrorDao errorDao;

    @ExceptionHandler(value = Exception.class)
    @ResponseBody
    public ResponseEntity<?> jsonErrorHandler(HttpServletRequest req, Exception ex) {
        System.out.println(ex);
        //记录错误
        ErrorInfo errorInfo = new ErrorInfo();
        CommonHelper.getId(errorInfo);
        errorInfo.setMessage(ex.getMessage());
        errorInfo.setDetail(CommonHelper.getExceptionDetail(ex));
        errorDao.save(errorInfo);
        //返回值
        ResponseInfo responseInfo = new ResponseInfo(404, "错误代码：" + errorInfo.getNum());
        return ResponseEntity.ok(responseInfo);
    }
}
