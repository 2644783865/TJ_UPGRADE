<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_OrgDataInput.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_OrgDataInput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../Controls/UserDefinedQueryConditions.ascx" tagname="UserDefinedQueryConditions" tagprefix="uc3" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<base target="_self" />
<head id="Head1" runat="server">
    <title>已输入原始数据</title>
     <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
</head>
<body>
     <script language="javascript" type="text/javascript">
        var ret="true";
        var n=0;
        function Common()
        {
           var table=document.getElementById("<%=grv.ClientID%>");
           var tr=table.getElementsByTagName("tr");
           var checkbox;
           var bmid;
           arrList = new Array();
           var index=0;
           for(i=1;i<tr.length;i++)
           {
              checkbox=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
              bmid=tr[i].getElementsByTagName("td")[1].getElementsByTagName("input")[0];
              if(checkbox.checked)
              {
                 arrList[index]=bmid.value;
                 index++;
              }
           }
        }
        function bulkeditverify()  //验证批量复制的是否提交制作明细和材料计划，外协等
        {
           var table=document.getElementById("<%=grv.ClientID%>");
           var tr=table.getElementsByTagName("tr");
           for(i=1;i<tr.length;i++)
           {
              var cb=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
              if(cb.checked)
              {
                 n++;
                 var statetext=tr[i].getElementsByTagName("td")[1].getElementsByTagName("input")[1];
                 arrstate=new Array();
                 arrstate=(statetext.value).split("-");
                 if(arrstate[1]!="0"||arrstate[3]!="0"||arrstate[5]!="0")
                 {
                    ret="false";
                    break;
                 }
                 else
                 {
                    if(arrstate[0]!="0"||arrstate[2]!="0"||arrstate[4]!="0")
                    {
                       ret="false";
                       break;
                    }
                    else
                    {
                       ret="true";
                    }
                 }
              }
           }
        }
        function ShowOrgInputed()
        {
           Common()
           var date=new Date();
           var time=date.getTime();
           var aa=window.showModalDialog("TM_BachCopy.aspx?action=<%=tsaid.Text %>&NoUse="+time+"&arry="+arrList[0],'obj',"dialogHeight:"+screen.availHeight+";dialogWidth:"+screen.availWidth+";status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
           if(aa=="Refesh")
           {
              window.returnValue="Refesh";
              window.location.reload();
           }
        }
        
        function ShowBulkCopy()
        {
           var aa
           Common();
           bulkeditverify();
           if(n==0)
           {
              alert('请勾选需要批量复制的记录！');
              return;
           }
           else
           {
               var date=new Date();
               var time=date.getTime();
               aa=window.showModalDialog("TM_BulkCopy.aspx?action=<%=tsaid.Text %>&NoUse="+time+"&arry="+arrList,'obj',"dialogHeight:"+screen.availHeight+";dialogWidth:"+screen.availWidth+";status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
               ////////window.open("TM_BulkCopy.aspx?action=<%=tsaid.Text %>&NoUse="+time+"&arry="+arrList,"_self","");
           }
           if(aa=="Refesh")
           {
              window.returnValue="Refesh";
              window.location.reload();
           }
        }
        
        function ShowBulkEidt()
        {
           var aa
           Common();
           bulkeditverify();
           if(n==0)
           {
              alert('请勾选需要批量修改的任务！');
              return;
           }
           else
           {
              if(ret=="true")
              {
                 var date=new Date();
                 var time=date.getTime();
                 aa=window.showModalDialog("TM_BulkEdit.aspx?action=<%=tsaid.Text %>&NoUse="+time+"&arry="+arrList,'obj',"dialogHeight:"+screen.availHeight+";dialogWidth:"+screen.availWidth+";status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
                 ///////window.open("TM_BulkEdit.aspx?action=<%=tsaid.Text %>&NoUse="+time+"&arry="+arrList,"_self","");
              }
              else
              {
                  alert('不能勾选已提交过的记录！！！');
                  return;
              }
           }
           if(aa=="Refesh")
           {
              window.returnValue="Refesh";
              window.location.reload();
           }
        }
        
        function SelectAll(obj)
        {
             var table=document.getElementById("grv");
             if(obj.checked)
             {
                 for(i=1;i<table.rows.length;i++)
                 {
                    objstr=table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("span")[0].getElementsByTagName("input")[0];
                    objstr.checked=true;
                 }
             }
             else
             {
                for(i=1;i<table.rows.length;i++)
                {
                    objstr=table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("span")[0].getElementsByTagName("input")[0];
                    objstr.checked=false;
                }
             }
        }
     </script>
    <form id="form1" runat="server">
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div class="box-inner">
     <div class="box_right">
       <div class="box-title">
           <table width="100%">
             <tr>
               <td style="width:30%">任务号：
                <asp:Label ID="tsaid" runat="server"></asp:Label>
               </td>
               <td style="width:25%">合同号： <asp:Label ID="lblContract" runat="server"></asp:Label> </td>
             <td style="width:30%">项目名称：
                <asp:Label ID="proname" runat="server"></asp:Label>
               
            </td>
            <td style="width:30%">设备名称：
                <asp:Label ID="engname" runat="server"></asp:Label>
                <input id="eng_type" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td align="right">
                <input id="btnClose" type="button" onclick="window.close();" value="关 闭" />&nbsp;&nbsp;&nbsp;&nbsp;</td>
             </tr>
           </table>
       </div>
     </div>
   </div>
   
    <div class="box-wrapper">
        <div class="box-outer">
        <table width="100%">
        <tr>
        <td align="right" style="width:10%;">部件名称:</td>
           <td align="left" valign="top" style="width:20%;height:42px">             
             <cc1:ComboBox ID="ddlbjname" runat="server" AutoPostBack="true" Height="15px"
                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                        onselectedindexchanged="ddlbjname_SelectedIndexChanged">
                        </cc1:ComboBox>
           </td>
           <td align="right">
            材料名称:
             <asp:DropDownList ID="ddlname" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlname_SelectedIndexChanged">
                <asp:ListItem Text="-请选择-" Value="0" Selected="True"></asp:ListItem>
            </asp:DropDownList>
            </td><td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            材料规格:
            <asp:DropDownList ID="ddlguige" runat="server" AutoPostBack="True"
                onselectedindexchanged="ddlguige_SelectedIndexChanged">
                <asp:ListItem Text="-请选择-" Value="0" Selected="True"></asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            排序:
            <asp:DropDownList ID="ddlSort" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnQuery_OnClick">
             <asp:ListItem Text="综合" Value="BM_ORDERINDEX"></asp:ListItem>
              <asp:ListItem Text="序号" Value="BM_XUHAO"></asp:ListItem>
              <asp:ListItem Text="总序" Value="BM_ZONGXU" Selected="True"></asp:ListItem>
            </asp:DropDownList>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             显示级数:
           <asp:DropDownList ID="ddlOrgJishu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrgjishu_SelectedIndexChanged">
           <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
             <asp:ListItem Text="1级" Value="0"></asp:ListItem>
             <asp:ListItem Text="2级" Value="1"></asp:ListItem>
             <asp:ListItem Text="3级" Value="2"></asp:ListItem>
             <asp:ListItem Text="4级" Value="3"></asp:ListItem>
             <asp:ListItem Text="5级" Value="4"></asp:ListItem>
             <asp:ListItem Text="6级" Value="5"></asp:ListItem>
             <asp:ListItem Text="7级" Value="6"></asp:ListItem>
             <asp:ListItem Text="8级" Value="7"></asp:ListItem>
           </asp:DropDownList>&nbsp;<asp:DropDownList ID="ddlShowType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrgjishu_SelectedIndexChanged">
            <asp:ListItem Text="按总序" Value="BM_ZONGXU" Selected="True"></asp:ListItem>
            <asp:ListItem Text="按序号" Value="BM_XUHAO"></asp:ListItem>
                </asp:DropDownList>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:HyperLink ID="HyperLink5" CssClass="hand" runat="server"><asp:Image ID="Image8" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2"  align="absmiddle" runat="server" />其它筛选</asp:HyperLink>
        <cc1:PopupControlExtender  ID="PopupControlExtender5" CacheDynamicResults="false" Position="Left"  Enabled="true" runat="server" OffsetX="-300"  OffsetY="-55"  TargetControlID="HyperLink5" PopupControlID="palMS3">
        </cc1:PopupControlExtender>
        <asp:Panel ID="palMS3" style="display:none;visibility:hidden;border-style:solid;border-width:1px;border-color:blue;background-color:Menu;" runat="server">
        <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Always" runat="server">
        <ContentTemplate>
        <table>
         <tr>       
         <td>
              <div style="font-family: Verdana, Helvetica, Arial, sans-serif;line-height: 17px;font-size: 11px;font-weight: bold;position: absolute;top:8px;right: 10px;">
                  <a onclick="document.body.click(); return false;" style="background-color: #6699CC; cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;" title="关闭">X</a>
              </div>
              <br /><br />
         </td>
         </tr> 
            <tr>
            <td align="right"><asp:Button ID="btnQuery1" runat="server" UseSubmitBehavior="false" OnClick="btnQuery_OnClick" Text="查 询" />
            &nbsp;&nbsp;<asp:Button ID="Button2" runat="server" UseSubmitBehavior="false" OnClick="btnMSClear_OnClick" Text="清 空" />&nbsp;&nbsp;</td>
         </tr>
          <tr>
          <td align="left" style="width:98%">
             <uc3:UserDefinedQueryConditions runat="server" ID="udqMS" QueryColumnsType="TaskView"  QueryRows="8" />
          </td>
          </tr>
        </table>        
        </ContentTemplate>
        </asp:UpdatePanel>
        </asp:Panel>
           </td>
        </tr>
        <tr>
          <td align="left" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;全选/取消<input id="ckbSelectAll" onclick="SelectAll(this);" type="checkbox" />&nbsp;&nbsp;
              <asp:Button ID="btnSelect" ToolTip="选择起止项后点击连选,起止项间所有记录选中" runat="server" OnClick="btnSelect_OnClick" Text="连选" />
          <span style="color:Red">鼠标勾选要操作的记录</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td> 
            <td align="right">查询类别:<asp:DropDownList ID="ddpQueryType" runat="server">
                  <asp:ListItem Text="-请选择-" Value="0" Selected="True" ></asp:ListItem>
                  <asp:ListItem Text="总序" Value="BM_ZONGXU"></asp:ListItem>
                  <asp:ListItem Text="序号" Value="BM_XUHAO"></asp:ListItem> 
                  <asp:ListItem Text="中文名称" Value="BM_CHANAME"></asp:ListItem>
                  <asp:ListItem Text="图号" Value="BM_TUHAO"></asp:ListItem>
                  <asp:ListItem Text="物料编码" Value="BM_MARID"></asp:ListItem> 
                  <asp:ListItem Text="规格" Value="BM_GUIGE"></asp:ListItem> 
                  <asp:ListItem Text="材质" Value="BM_MAQUALITY"></asp:ListItem>   
              </asp:DropDownList></td><td>
               <asp:TextBox ID="txtItem" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;
              <input id="btn" type="button" style="display:none;" visible="false" value="单条复制-功能已取消" onclick="ShowOrgInputed();" title="勾选或者直接单击进行单条复制！" />&nbsp;&nbsp;
              <input id="btnCopy" type="button" value="批量复制" onclick="ShowBulkCopy();" title="勾选需要批量复制的任务！" visible="false"  />&nbsp;&nbsp;
              <input id="btnedit" type="button" value="批量修改" onclick="ShowBulkEidt();" title="勾选没有提交过的任务进行批量修改！" visible="false" />
          </td>
        </tr>
        </table>
      <span id="mySpan" style="overflow:scroll;" > 
       <yyc:SmartGridView ID="grv" runat="server" AutoGenerateColumns="False" CssClass="toptable grid" 
             CellPadding="4" ForeColor="#333333" Width="100%" onrowdatabound="grv_RowDataBound" DataKeyNames="BM_XUHAO">
        <RowStyle BackColor="#EFF3FB"/>
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Width="10px"  CssClass="checkBoxCss"/>
                </ItemTemplate>
                <ItemStyle Width="10px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="行号" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                    <input id="bmid" runat="server" style="display:none;" type="hidden" value='<%#Eval("BM_XUHAO") %>' />
                    <asp:HiddenField ID="hdfOrgState"  runat="server" Value='<%#Eval("BM_MPSTATE").ToString()+"-"+Eval("BM_MPSTATUS").ToString()+"-"+Eval("BM_MSSTATE").ToString()+"-"+Eval("BM_MSSTATUS").ToString()+"-"+Eval("BM_OSSTATE").ToString()+"-"+Eval("BM_OSSTATUS").ToString()+"-"+Eval("BM_CONDICTIONATR").ToString() %>' />
                    <asp:HiddenField ID="hdmarid"  runat="server" Value='<%#Eval("BM_MARID") %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="C" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField HeaderText="Z" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField HeaderText="W" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField DataField="BM_XUHAO" HeaderText="序号"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUHAO" HeaderText="图号" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MARID" HeaderText="物料编码" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_ZONGXU" HeaderText="总序" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_CHANAME" HeaderText="名称" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_GUIGE" HeaderText="规格" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAQUALITY" HeaderText="材质" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MALENGTH" HeaderText="材料长度" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAWIDTH" HeaderText="材料宽度" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="NUMBER" HeaderText="单台数量|总数量|计划数量" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAUNITWGHT" DataFormatString="{0:F2}" HeaderText="材料单重" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MATOTALWGHT" DataFormatString="{0:F2}" HeaderText="材料总重" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_UNITWGHT" DataFormatString="{0:F2}" HeaderText="单重" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TOTALWGHT" DataFormatString="{0:F2}" HeaderText="总重" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MATOTALLGTH" HeaderText="材料总长" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
        </Columns>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
        <FixRowColumn FixRowType="Header,Pager" TableHeight="600px" TableWidth="100%" FixColumns="0,1" />        
      </yyc:SmartGridView>
      </span>
            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center"><hr style="width:100%; height:0.1px; color:Blue;" />没有记录!
            </asp:Panel>
       <uc1:UCPaging ID="UCPaging1" runat="server" />
    </div>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
    <ProgressTemplate>
       <div style="position: absolute; top: 40%; right:40%">
       <table>
       <tr>
       <td align="right"><asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" /></td>
       <td align="left" style="background-color:Yellow; font-size:medium;">数据处理中，请稍后...</td>
       </tr>
       </table>
       </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    </form>
</body>
</html>

