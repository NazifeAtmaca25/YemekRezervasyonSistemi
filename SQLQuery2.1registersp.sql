create proc register_sq
(
@tc varchar(50)
)
as
begin
 select count(*) from users where @tc=tc 
end

