<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WL_Material_List.aspx.cs"  MasterPageFile="~/Masters/BaseMaster.Master" Inherits="ZCZJ_DPF.Basic_Data.WL_Material_List" Title="物料信息维护"%>
<%@ Register src="~/Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

 <asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
 <script src="../JS/DatePicker.js" type="text/javascript"></script>
 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
 <script  type="text/javascript" language="javascript">
  function DefaultTextOnFocus(obj)
  {
     if(obj.value=="请输入查询关键字！")
     {
        obj.value="";
        obj.style.color="Black";
     }
     else
     {
       obj.style.color="Black";
     }
  }
  
  function DefaultTextOnBlur(obj)
  {
     if(obj.value=="")
     {
        obj.value="请输入查询关键字！";
        obj.style.color="Gray";
     }
  }
</script>
         <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div class="box-inner">
        <div class="box_right">
        <div class="box-title">
            
            <table width="100%">
                <tr>
                    <td>选择物料大类：
                    <asp:DropDownList ID="DDLclass" runat="server" AutoPostBack ="true" 
                            onselectedindexchanged="DDLclass_SelectedIndexChanged">
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 选择物料种类：
                    <asp:DropDownList ID="DDLname" runat="server" AutoPostBack ="true" 
                            onselectedindexchanged="DDLname_SelectedIndexChanged">
                    </asp:DropDownList>&nbsp;&nbsp; 
                    </td>
                    <td  height="25" align="right">状态:</td>     
                <td>
                    <asp:RadioButtonList ID="rblZT" RepeatColumns="2" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="rblZT_SelectedIndexChanged">
                    <asp:ListItem Value="0">停用</asp:ListItem>
                    <asp:ListItem Selected="True" Value="1">在用</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            
                    <td align="left">
                     <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server"><asp:Image ID="Image4" ImageUrl="../Assets/icon-fuction/388.gif" border="0" hspace="2" 
                                        align="absmiddle" runat="server" />筛选</asp:HyperLink>
                     <asp:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false" Position="Bottom"  Enabled="true" runat="server" OffsetX="-300"  OffsetY="8"  TargetControlID="HyperLink1" PopupControlID="palORG">
                     </asp:PopupControlExtender>
                      <asp:Panel ID="palORG" Width="500px" style="display:none;visibility:hidden;border-style:solid;border-width:1px;border-color:blue;background-color:Menu;" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>  
                     <table width="100%" >
                     <tr>       
                     <td>
                          <div style="font-family: Verdana, Helvetica, Arial, sans-serif;line-height: 17px;font-size: 11px;font-weight: bold;position: absolute;top:8px;right: 10px;">
                              <a onclick="document.body.click(); return false;" style="background-color: #6699CC; cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;" title="关闭">X</a>
                          </div>
                          <br /><br />
                     </td>
                     </tr>
                     <tr>
                     <td>模糊编码：</td>
                     <td>
                     <asp:TextBox ID="txt_ID2" runat="server"></asp:TextBox>
                     </td>
                     <td>维护时间</td>
                     <td><input id="date_ID" runat="server" type="text" onclick="setday(this)" value="" readonly="readonly" /></td>
                     </tr>
                        <tr>
                         <td >精确编码:</td>
                          <td >
                              <asp:TextBox ID="txt_ID" runat="server"></asp:TextBox>
                        </td> 
                        <td>到</td>
                        <td>
                        <asp:TextBox ID="txt_ID1" runat="server"></asp:TextBox>
                        </td>
                        </tr>
                       <tr>
                          <td >名称:</td>
                          <td >
                              <asp:TextBox ID="txt_NAME" runat="server" ></asp:TextBox>
                        </td> 
                         <td>规格:</td>
                          <td>
                              <asp:TextBox ID="txt_GG" runat="server"></asp:TextBox>
                        </td>
                        <tr>
                          <td>助记码:</td>
                          <td>
                             <asp:TextBox ID="txt_ZJM" runat="server"></asp:TextBox>
                        </td>
                       <td>材质:</td>
                          <td>
                              <asp:TextBox ID="txt_CZ" runat="server"></asp:TextBox>
                        </td>
                        <tr>
                         <td>国标:</td>
                          <td>
                              <asp:TextBox ID="txt_GB" runat="server"></asp:TextBox>
                        </td>
                       <td>维护人:</td>
                      <td><asp:DropDownList ID="ddlWFName" runat="server"></asp:DropDownList>
                      </td>
                      </tr>
                      <tr>
                      <td>排序方式</td>
                      <td><asp:RadioButtonList ID="txt_px" runat="server" RepeatDirection="Horizontal" RepeatColumns="2">
                      <asp:ListItem Text="升序" Value="0"></asp:ListItem>
                      <asp:ListItem Text="降序" Value="1"></asp:ListItem></asp:RadioButtonList></td>
                      </tr>
                      <tr>
                      <td>排序依据列</td>
                      <td><asp:DropDownList ID="txt_pxl" runat="server">
                      <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                      <asp:ListItem Text="编号" Value="1"></asp:ListItem>
                      <asp:ListItem Text="中文名称" Value="2"></asp:ListItem>
                      <asp:ListItem Text="英文名称" Value="3"></asp:ListItem>
                      <asp:ListItem Text="助记码" Value="4"></asp:ListItem>
                      <asp:ListItem Text="规格" Value="5"></asp:ListItem>
                      <asp:ListItem Text="材质" Value="6"></asp:ListItem>
                      <asp:ListItem Text="国标" Value="7"></asp:ListItem>
                      <asp:ListItem Text="维护时间" Value="8"></asp:ListItem></asp:DropDownList></td>
                      </tr>
                     <tr>
                         <td colspan="4" align="center">
                             <asp:Button ID="btn_confirm" runat="server" Text="查询" UseSubmitBehavior="false"  OnClick="btn_confirm_Click"/>&nbsp;&nbsp; 
                             <asp:Button ID="btn_clear" runat="server" Text="置空" UseSubmitBehavior="false"   OnClick="btn_clear_Click"/>
                         </td>
                     </tr>
                     </table>
                      </ContentTemplate>
                    </asp:UpdatePanel>
                    </asp:Panel> 
                    
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    </td> 
                    
                    <td>每页显示：<asp:DropDownList ID="ddl_pageno" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                        <asp:ListItem Text="100" Value="100"></asp:ListItem>
                  </asp:DropDownList>&nbsp;条记录&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  </td>
                    
                     <td>
                    <asp:Button ID="daochu" runat="server" OnClick="btnExport_OnClick" OnClientClick="return confirm('确认导出吗？')" Text="导出"/>
                    </td>

                    <td align="right">
                        <asp:HyperLink ID="HyperLinkAdd" NavigateUrl="javascript:void window.open('WL_Material_Edit.aspx?edit=Edit','添加物料信息','');" runat="server" >
                        <asp:Image ID="ImageAdd" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                        添加物料信息</asp:HyperLink>&nbsp;&nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="删除"  onclick="btnDelete_Click" Visible="false" />&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
        </div>
        </div>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
<Triggers>
 <asp:PostBackTrigger ControlID="btn_confirm" />
 <asp:PostBackTrigger ControlID="btn_clear" />
</Triggers>
<ContentTemplate>
        <div class="box-wrapper">
        <div class="box-outer">
        <div style="width:100%; height:auto; overflow:scroll; overflow-y:auto; overflow-x:yes; display:block;">
        <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
            <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
                <tr align="center" class="tableTitle" >                  
                    <td class="notbrk"><strong>序号</strong></td>
                    <td class="notbrk"><strong>编号</strong></td>
                    <td class="notbrk"><strong>中文名称</strong></td> 
                    <td class="notbrk"><strong>英文名称</strong></td>                
                    <td class="notbrk"><strong>助记码</strong></td>
                    <td class="notbrk"><strong>规格</strong></td>
                    <td class="notbrk"><strong>材质</strong></td>
                    <td class="notbrk"><strong>国标</strong></td> 
                    <td class="notbrk"><strong>理论重量</strong></td>
                    <td class="notbrk"><strong>米面积</strong></td>                   
                    <td class="notbrk"><strong>技术单位</strong></td>
                    <td class="notbrk"><strong>转换率</strong></td>
                    <td class="notbrk"><strong>采购单位</strong></td>                   
                    <td class="notbrk"><strong>辅助单位</strong></td>
                    <td class="notbrk"><strong>最近维护人</strong></td>
                    <td class="notbrk"><strong>维护时间</strong></td>
                    <td class="notbrk"><strong>修改</strong></td>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr align="center" class="baseGadget" onMouseOver="this.className='highlight'" onMouseOut="this.className='baseGadget'" style="text-align:left">
<%--                    <td><asp:CheckBox ID="CheckBox1" CssClass="checkBoxCss" runat="server" Visible="false" />
                    <asp:Label ID="labID" runat="server" visible="false" Text='<%#Eval("id")%>'></asp:Label></td>
--%>                    <td><asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>'></asp:Label></td>
                    <td class="notbrk"><%#Eval("id")%>&nbsp;</td>
                    <td class="notbrk"><%#Eval("MNAME")%>&nbsp;</td> 
                    <td class="notbrk"><%#Eval("MENGNAME")%>&nbsp;</td>                        
                    <td class="notbrk"><%#Eval("HMCODE")%>&nbsp;</td>
                    <td class="notbrk"><%#Eval("GUIGE")%>&nbsp;</td>
                    <td class="notbrk"><%#Eval("CAIZHI")%>&nbsp;</td>
                    <td class="notbrk"><%#Eval("GB")%>&nbsp;</td>
                    <td class="notbrk"><%#Eval("MWEIGHT")%>&nbsp;</td>
                    <td class="notbrk"><%#Eval("MAREA")%>&nbsp;</td>
                    <td class="notbrk"><%#Eval("TECHUNIT")%>&nbsp;</td>
                    <td class="notbrk"><%#Eval("CONVERTRATE")%>&nbsp;</td>
                    <td class="notbrk"><%#Eval("PURCUNIT")%>&nbsp;</td>
                    <td class="notbrk"><%#Eval("FUZHUUNIT")%>&nbsp;</td> 
                    <td class="notbrk"><%#Eval("ST_NAME")%>&nbsp;</td> 
                    <td class="notbrk"><%#Eval("FILLDATE").ToString().Split(' ')[0]%>&nbsp;</td>                  
                    <td class="notbrk"><asp:HyperLink ID="HyperLinkModify"  NavigateUrl='<%# editWl(Eval("id").ToString()) %>' runat="server">
                    <asp:Image ID="ImageModify" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />修改</asp:HyperLink></td>   
                </tr>
            </ItemTemplate>
            </asp:Repeater>
            <asp:Panel ID="NoDataPanel" runat="server" Visible="False">没有记录!</asp:Panel>
        </table>
        </div></div>    
                <div class="PageChange">
         <uc1:UCPaging ID="UCPaging1" runat="server" />

        </div>         
        </div>
        </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>