<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master" CodeBehind="TM_MarReplaceBulk.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MarReplaceBulk" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register src="../Controls/UserDefinedQueryConditions.ascx" tagname="UserDefinedQueryConditions" tagprefix="uc3" %>
<%@ Register src="../Controls/JSRegister.ascx" tagname="JSRegister" tagprefix="JSR" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">物料替换
   <link href="../Assets/AutoCompleteTextBox.css" rel="stylesheet" type="text/css" /> 
   <script src="../JS/MS_Adjust.js" type="text/javascript" charset="GB2312"></script>
   <script src="../JS/SQL.js" type="text/javascript" charset="GB2312"></script>
   <JSR:JSRegister ID="JSRegister1" runat="server" />
   <script language="javascript" type="text/javascript">
    function QueryCheck()
    {
       var table=document.getElementById("<%=GridView2.ClientID %>");
       if(table==null)
       {
          return true;
       }
       else
       {
           if(table.rows.length>0)
           {
              return confirm("页面上已存在数据，查询将重置数据！！！\r\r提示:确认继续吗？");
           }
           else
           {
              return true;
           }
       }
    }
    function SubmitCheck()
    {
       var table=document.getElementById("<%=GridView2.ClientID %>");
       if(table==null)
       {
          alert("没有可替换数据！！！");
          return false;
       }
       else
       {
           var rowcount=table.rows.length;
           if(rowcount>0)
           {
             var oldshape="";
             var oldstate="";
             var newshape="";
             var newstate="";
             
             var marid="";

              for(var i=1;i<rowcount;i++)
              {
                 oldshape=table.rows[i].cells[6].innerHTML;
                 oldstate=table.rows[i].cells[7].innerHTML;
                 newshape=table.rows[i].cells[9].getElementsByTagName("input")[0].value;
                 newstate=table.rows[i].cells[10].getElementsByTagName("input")[0].value;
                 
                 marid=table.rows[i].cells[8].getElementsByTagName("input")[0].value;
                 
                 if(marid=="")
                 {
                    alert("第【"+i+"】行请输入【物料编码】!!!");
                    return false;
                 }
                 if(newshape=="")
                 {
                    alert("第【"+i+"】行请输入【毛坯形状】!!!");
                    return false;
                 }
                 if(newshape=="采"&&newstate=="")
                 {
                    alert("第【"+i+"】行【毛坯形状:"+newshape+"】，必须输入【毛坯状态】!!!");
                    return false;
                 }
                 
                 if(newshape!=oldshape)
                 {
                    alert("无法替换!!!\r\r提示:第【"+i+"】行替换前【毛坯形状:"+oldshape+"】与替换后【毛坯形状:"+newshape+"】不一致");
                    return false;
                 }
              }
              return confirm("确认替换吗？\r\r重要提示:请您在替换前【双击某一行】查看该物料的替换信息！！！");
           }
           else
           {
              alert("没有可替换数据！！！");
              return false;
           }
       }
    }
    function ShowReplaceDetail(taskid,marid)
    {
       var date=new Date();
       var time=date.getTime();
       window.open("TM_ShowReplaceDetail.aspx?NoUse="+time+"&TaskID="+taskid+"&Marid="+marid);
    }
   </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                      <table width="100%">
                      <tr>
                      <td align="center">合同号:<asp:Label ID="lblContratId"  runat="server" ></asp:Label></td>&nbsp&nbsp
                      <td align="center">项目名称:<asp:Label ID="lblProjName" runat="server" ></asp:Label></td>&nbsp&nbsp
                        <td align="center">设备名称:<asp:Label ID="lblEngName"  runat="server" ></asp:Label>
                       
                         
                        </td>
                        <td>  <input type="hidden" id="hdfEngid" runat="server" /></td>
                      </tr>
                      </table>
                    </div>
                </div> 
           </div>
               <div class="box-wrapper">
        <div class="box-outer">
      <table width="100%">
      <tr>
        <td colspan="10"><strong>请输入要替换的物料信息:</strong></td>
      </tr>
      <tr>
      <td align="right" class="notbrk">物料编码:</td><td align="left"><asp:TextBox ID="txtMarid" onfocus="this.select();" runat="server"></asp:TextBox></td>
      <td align="right" class="notbrk">物料名称:</td><td align="left"><asp:TextBox ID="txtMarName"  onfocus="this.select();" runat="server"></asp:TextBox></td>
      <td align="right" class="notbrk">物料规格:</td><td align="left"><asp:TextBox ID="txtGuiGe"  onfocus="this.select();" runat="server"></asp:TextBox></td>
      <td align="right" class="notbrk">物料材质:</td><td align="left"><asp:TextBox ID="txtMarCZ"  onfocus="this.select();" runat="server"></asp:TextBox></td>
      <td rowspan="2" align="center"><asp:Button ID="btnQuery" runat="server" Text="查 询" OnClientClick="return QueryCheck();" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;&nbsp;
           <asp:Button ID="btnClear" runat="server" Text="清 空" OnClick="btnClear_OnClick" />
           &nbsp;&nbsp;&nbsp;<asp:Button ID="btnReplace" runat="server" Text="替 换" OnClientClick="return SubmitCheck();" OnClick="btnReplace_OnClick" /></td>
      </tr>
      <tr>
       <td align="right">总&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;序:</td><td align="left"><asp:TextBox ID="txtZongXu" runat="server"  onfocus="this.select();"></asp:TextBox></td>
       <td align="right"></td><td align="left"></td>
       <td align="right"></td><td align="left"></td>
       <td colspan="2" align="center"><asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server"><asp:Image ID="Image4" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2" 
                    align="absmiddle" runat="server" />其它筛选</asp:HyperLink>
        <cc1:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false" Position="Left"  Enabled="true" runat="server" OffsetX="-400"  OffsetY="-55"  TargetControlID="HyperLink1" PopupControlID="palORG">
        </cc1:PopupControlExtender>
       <asp:Panel ID="palORG" Width="90%" style="display:none;visibility:hidden;border-style:solid;border-width:1px;border-color:blue;background-color:Menu;" runat="server">
         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
          <Triggers><asp:PostBackTrigger ControlID="btnQueryUser" /></Triggers>
          <Triggers><asp:PostBackTrigger ControlID="btnClearUser" /></Triggers>
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
          <td align="right">
             <asp:Button ID="btnQueryUser" runat="server" Text="查 询" UseSubmitBehavior="false"   OnClick="btnQuery_OnClick" />&nbsp;&nbsp;&nbsp;
             <asp:Button ID="btnClearUser" runat="server" Text="清 空" UseSubmitBehavior="false" OnClick="btnClearUser_OnClick" />&nbsp;&nbsp;&nbsp;
          </td>
         </tr>
         <tr>
          <td><uc3:UserDefinedQueryConditions ID="udqOrg" runat="server" QueryColumnsType="TaskView"  QueryRows="8" /></td>
         </tr>
         </table>
         <br />
         </ContentTemplate>
         </asp:UpdatePanel>
      </asp:Panel>
       </td>
      </tr>
     <tr>
     <td colspan="10">
      <asp:Panel ID="NoDataPanel1" runat="server">
         <div style="text-align:center;">没有记录！</div>
      </asp:Panel>
     <yyc:SmartGridView ID="GridView2" CssClass="toptable grid" Width="100%" runat="server" onrowdatabound="GridView1_RowDataBound"  AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333">
       <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
       <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="Index" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField  HeaderText="物料编码" ItemStyle-HorizontalAlign="Center" DataField="BM_MARID" />
            <asp:BoundField  HeaderText="名称" ItemStyle-HorizontalAlign="Center" DataField="BM_MANAME" />
            <asp:BoundField  HeaderText="规格" ItemStyle-HorizontalAlign="Center" DataField="BM_MAGUIGE" />
            <asp:BoundField  HeaderText="材质" ItemStyle-HorizontalAlign="Center" DataField="BM_MAQUALITY" />
           
            <asp:BoundField  HeaderText="材料类别" ItemStyle-HorizontalAlign="Center" DataField="BM_MASHAPE" />
          
                
            <asp:TemplateField HeaderText="物料编码" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:TextBox ID="txtNewMarid" runat="server" onchange="AutoCodeReplace(this);" onfocus="this.select();"  Width="100"></asp:TextBox> 
                     <cc1:AutoCompleteExtender ID="marid_AutoCompleteExtender"   runat="server" CompletionSetCount="15" CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="HmCode" FirstRowSelected="true" 
                                ServicePath="~/Ajax.asmx" TargetControlID="txtNewMarid" UseContextKey="True" CompletionInterval="10">
                    </cc1:AutoCompleteExtender>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="材料类别" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:TextBox ID="txtNewMarShape" Width="40" runat="server" onfocus="this.select();"  onkeydown="grControlFocus(this);"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>    
            
            <asp:TemplateField HeaderText="计划系数" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:TextBox ID="txtMarNewXishu" Width="40" runat="server" onfocus="this.select();"  onblur='var pattem=/^\d+(\.\d+)?$/; if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="1";this.select();}' onkeydown="grControlFocus(this);"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>  
            <asp:TemplateField HeaderText="名称" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false">
                <ItemTemplate>
                </ItemTemplate>
            </asp:TemplateField>  
             <asp:TemplateField HeaderText="规格" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false">
                <ItemTemplate>
                </ItemTemplate>
            </asp:TemplateField>  
            <asp:TemplateField HeaderText="材质" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false">
                <ItemTemplate>
                </ItemTemplate>
            </asp:TemplateField>  
                        <asp:TemplateField HeaderText="标准" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false">
                <ItemTemplate>
                </ItemTemplate>
            </asp:TemplateField>  
         </Columns>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle Wrap="false" />
        <AlternatingRowStyle BackColor="White" />
        <FixRowColumn FixRowType="Header" TableHeight="500px"  FixColumns="" />
     </yyc:SmartGridView>
     </td>
     </tr>
    </table>
    </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="1">
        <ProgressTemplate>
            <div style="position: absolute; top: 60%; right: 40%">
                <table>
                    <tr>
                        <td align="right">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" />
                        </td>
                        <td align="left" style="background-color: Yellow; font-size: medium;">
                            数据处理中，请稍后...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
