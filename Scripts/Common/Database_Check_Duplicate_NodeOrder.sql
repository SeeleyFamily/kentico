-- checks every node parent for duplicate node orders as children
-- useful for debugging ordering issues
select nodeparentid,nodeorder,nodesiteid, count(*) from View_CMS_Tree_Joined 
group by nodeparentid,nodeorder,nodesiteid having count(*)>1 
order by nodeparentid

--

select nodesiteid,* from view_cms_tree_joined where nodeparentid in
(
select nodeparentid from View_CMS_Tree_Joined 
group by nodeparentid,nodeorder,nodesiteid having count(*)>1 
)and nodeorder=1
--
