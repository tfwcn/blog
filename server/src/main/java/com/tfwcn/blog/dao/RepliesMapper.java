package com.tfwcn.blog.dao;

import com.tfwcn.blog.models.Replies;
import org.apache.ibatis.annotations.Mapper;

import java.util.List;

@Mapper
public interface RepliesMapper {
    int deleteByPrimaryKey(String id);

    int insert(Replies record);

    Replies selectByPrimaryKey(String id);

    List<Replies> selectAll();

    int updateByPrimaryKey(Replies record);
}