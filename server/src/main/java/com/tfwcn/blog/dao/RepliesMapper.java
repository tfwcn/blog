package com.tfwcn.blog.dao;

import com.tfwcn.blog.models.Replies;
import java.util.List;
import org.apache.ibatis.annotations.Mapper;

@Mapper
public interface RepliesMapper {
    int deleteByPrimaryKey(String id);

    int insert(Replies record);

    Replies selectByPrimaryKey(String id);

    List<Replies> selectAll();

    int updateByPrimaryKey(Replies record);
}