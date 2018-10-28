package com.tfwcn.blog.dao;

import com.tfwcn.blog.models.Errors;
import org.apache.ibatis.annotations.Mapper;

import java.util.List;

@Mapper
public interface ErrorsMapper {
    int deleteByPrimaryKey(String id);

    int insert(Errors record);

    Errors selectByPrimaryKey(String id);

    List<Errors> selectAll();

    int updateByPrimaryKey(Errors record);
}