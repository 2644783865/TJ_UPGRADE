<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Md_Detail_List.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Md_Detail_List" %>

<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    制作明细      
</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
  <%--  <script type="text/javascript" language="javascript">
    
    /*追加行提示*/
    function add()
    {
        var num=document.getElementById("<%=txtnum.ClientID%>").value;
        if(num!="")
        {
            document.getElementById("<%=addId.ClientID%>").value="1";
        }
        else
        {
            document.getElementById("<%=addId.ClientID%>").value="0";
            alert('请输入行数!');
            return false;
        }
    } 
       
    /*插入行提示*/
    function insert()
    {
        var obj=document.getElementsByTagName("input");
        var objstr = '';
        for(var i=0;i<obj.length;i++)
        {
            if(obj[i].type.toLowerCase()=="checkbox" && obj[i].value!="")
            {
               if( obj[i].checked)
               {    
                  objstr="checked";
               }
            }
        }
        if(objstr=="checked")
        {
            document.getElementById("<%=istid.ClientID%>").value="1" ;
        }
        else            
        {
            alert("请指定要插入行的位置!");
            return false;
        }
    }
    
    /*确定是否删除*/
    function check()
    {
        var obj=document.getElementsByTagName("input");
        var objstr = '';
        for(var i=0;i<obj.length;i++)
        {
            if(obj[i].type.toLowerCase()=="checkbox" && obj[i].value!="")
            {
                if( obj[i].checked)
                {    
                    objstr="checked";
                }
            }
        }
        if(objstr=="checked")
        {
           var i=confirm('确定删除吗？');
           if(i==true)
           {
                document.getElementById("<%=txtid.ClientID%>").value="1" ;
           }
           else
           {
                document.getElementById("<%=txtid.ClientID%>").value="0" ;
                return false;
           }
        }
        else            
        {
            alert("请选择要删除的行！");
            return false;
        }
    }
    </script>
--%>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager> 
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div class="box-inner">
    <div class="box_right">
    <div class="box-title">
        <table width="100%">
        <tr>
            <td style="width:18%">生产制号：
                <asp:Label ID="tsa_id" runat="server"></asp:Label>
                <input id="ms_list" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
                <td style="width:18%">项目名称：
                <asp:Label ID="lab_proname" runat="server"></asp:Label>
                <input id="ms_no" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
                <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
                <input id="status" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td style="width:18%">工程名称：
                <asp:Label ID="lab_engname" runat="server"></asp:Label>
                <input id="eng_id" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td align="right">
               <%-- 行数：<asp:TextBox ID="txtnum" runat="server" Width="40px"></asp:TextBox>&nbsp;
                <asp:Button ID="btnadd" runat="server" Text="追 加" OnClientClick="return add()" onclick="btnadd_Click" />&nbsp;
                <asp:Button ID="btninsert" runat="server" Text="插 入" OnClientClick="return insert()" onclick="btninsert_Click" />
                &nbsp;
                <asp:Button ID="btndelete" runat="server" Text="删 除" OnClientClick="return check()" onclick="btndelete_Click" />
                &nbsp;--%>
                <asp:Button ID="btnreview" runat="server" Text="下推审核" onclick="btnreview_Click"/>&nbsp;
                <%--<asp:Button ID="packlist" runat="server" Text="装箱单" OnClientClick="return confirm(&quot;确定生成装箱单？&quot;)" onclick="packlist_Click"/>&nbsp;--%>
                <asp:Button ID="btnreturn" runat="server" Text="返 回" onclick="btnreturn_Click" />
            </td>
         </tr>
        </table>
       </div>
     </div>
</div>

 <div class="box-wrapper">
        <%--<div class="box-outer">--%>
        <asp:Panel ID="Panel1" runat="server" style="height:564px; width: 99%; overflow: scroll; position: absolute">
           <%-- <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid" border="1">--%>
                <asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333">
                   <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                    <%-- <asp:TemplateField ItemStyle-Width="10px">
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" Width="10px" CssClass="checkBoxCss"/>
                        </ItemTemplate>
                        <ItemStyle Width="10px" />
                    </asp:TemplateField>
                     <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="ID" runat="server" Text='<%# Eval("MS_ID") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Width="30px" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input ID="Index" runat="server" style="border-style:none; width:80px" 
                                    type="text" value='<%#Eval("MS_INDEX") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="图号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input ID="tuhao" runat="server" style="border-style:none; width:120px" 
                                    type="text" value='<%#Eval("MS_TUHAO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="总序"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input ID="zongxu" runat="server" style="border-style:none; width:80px" 
                                    type="text" value='<%#Eval("MS_ZONGXU") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="名称" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input ID="Name" runat="server" style="border-style:none; width:120px" 
                                    type="text" value='<%#Eval("MS_NAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="规格" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input ID="Guige" runat="server" style="border-style:none; width:70px" 
                                    type="text" value='<%#Eval("MS_GUIGE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="材质" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input ID="Caizhi" runat="server" style="border-style:none; width:100px" 
                                    type="text" value='<%#Eval("MS_CAIZHI") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input ID="Num" runat="server" style="border-style:none; width:30px" 
                                    type="text" value='<%#Eval("MS_UNUM") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="单重" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="danzhong" runat="server" style="border-style:none; width:70px" 
                                    type="text" value='<%#Eval("MS_UWGHT") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                     <asp:TemplateField HeaderText="总重" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="zongzhong" runat="server" style="border-style:none; width:70px" 
                                    type="text" value='<%#Eval("MS_TLWGHT") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                     <asp:TemplateField HeaderText="毛坯" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="xinzhuang" runat="server" style="border-style:none; width:32px" 
                                    type="text" value='<%#Eval("MS_MASHAPE") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                     <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="zhuangtai" runat="server" style="border-style:none; width:32px" 
                                    type="text" value='<%#Eval("MS_MASTATE") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                     <asp:TemplateField HeaderText="标准" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="biaozhun" runat="server" style="border-style:none; width:120px" 
                                    type="text" value='<%#Eval("MS_STANDARD") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                     <asp:TemplateField HeaderText="工艺流程" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="gongyiliucheng" runat="server" style="border-style:none; width:90px" 
                                    type="text" value='<%#Eval("MS_PROCESS") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                     <asp:TemplateField HeaderText="箱号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="xianghao" runat="server" style="border-style:none; width:120px" 
                                    type="text" value='<%#Eval("MS_TIMERQ") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                     <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="remark" runat="server" style="border-style:none; width:100px" 
                                    type="text" value='<%#Eval("MS_NOTE") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="">
                      <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                       </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="MS_INDEX" HeaderText="序号" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MS_TUHAO" HeaderText="图号" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MS_ZONGXU" HeaderText="总序" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MS_NAME" HeaderText="名称" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MS_GUIGE" HeaderText="规格" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MS_CAIZHI" HeaderText="材质" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MS_UNUM" HeaderText="数量" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MS_UWGHT" DataFormatString="{0:N2}" HeaderText="单重(kg)" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MS_TLWGHT" DataFormatString="{0:N2}" HeaderText="总重(kg)" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MS_MASHAPE" HeaderText="毛坯" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MS_MASTATE" HeaderText="状态" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MS_STANDARD" HeaderText="国标" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MS_PROCESS" HeaderText="工艺流程" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MS_NOTE" HeaderText="备注" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <%--</table>--%>
            </asp:Panel>
            <%--<input id="addId" type="text"  runat="server" readonly="readonly" style="display: none" />
            <input id="txtid" type="text"  runat="server" readonly="readonly" style="display: none" />
            <input id="istid" type="text"  runat="server" readonly="readonly" style="display: none" />--%>
        <%--</div>--%>
</div>

</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
