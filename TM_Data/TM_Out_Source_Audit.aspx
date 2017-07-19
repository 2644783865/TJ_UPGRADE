<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Out_Source_Audit.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Out_Source_Audit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    技术外协审批     
</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
    <script language="javascript" type="text/javascript">
        //**********弹出技术部人员子窗口***********************
    var i;
    array=new Array();
    function SelTechPersons1()
    {
       i=window.showModalDialog('TM_Tech_persons.aspx?PerInChg=PIC','',"dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no");
       if(i!=null)
       {
           array=i.split(' ');
           document.getElementById('<%=txt_first.ClientID%>').innerText=array[0];
           document.getElementById('<%=firstid.ClientID%>').value=array[1];
       } 
       else
       {
           document.getElementById('<%=txt_first.ClientID%>').innerText="";
       }
    }
    function SelTechPersons2()
    {
        i=window.showModalDialog('TM_Tech_persons.aspx?PerInChg=PID','',"dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no"); 
        if(i!=null)
        {
            array=i.split(' ');
            document.getElementById('<%=txt_second.ClientID%>').innerText=array[0];
            document.getElementById('<%=secondid.ClientID%>').value=array[1];
        }
        else
       {
           document.getElementById('<%=txt_second.ClientID%>').innerText="";
       }
    }
    
    function SelTechPersons3()
    {
        i=window.showModalDialog('TM_Tech_persons.aspx?PerInChg=PIM','',"dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no"); 
        if(i!=null)
        {
            array=i.split(' ');
            document.getElementById('<%=txt_third.ClientID%>').innerText=array[0];
            document.getElementById('<%=thirdid.ClientID%>').value=array[1];
       }
       else
       {
           document.getElementById('<%=txt_third.ClientID%>').innerText="";
       }
    }
    function ShowDetail(index)
    {
        var newindex=index.substring(0,index.indexOf("-"));
        window.showModalDialog('TM_OUTBefore.aspx?LotNum=<%=osl_no1.Text %>&NewIndex='+newindex,'',"dialogHeight:300px;dialogWidth:1000px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no"); 
    }
    //双击查看明细
    function Dblclik_ShowDetail(tracknum)
    {
        var date=new Date();
        var time=date.getTime();
        var returnVlue=window.showModalDialog("TM_Out_Source_Audit_Detail.aspx?NoUse="+time+"&tracknum_table=" + encodeURIComponent(tracknum),'',"dialogHeight:400px;dialogWidth:900px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
    }
    
    function Submit()
    {
       var ok=confirm("提交后不能修改，确认提交吗？");
       if(ok==true)
       {
          if(document.getElementById("<%=btnsubmit.ClientID%>")==null)
          {
             alert("您无权提交！！！");
             return false;
          }
          else
          {
             document.getElementById("<%=btnsubmit.ClientID%>").click();
             document.getElementById("btnOutsubmit").disabled=true;
          }
       }
       return ok;
    }
   </script>
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
<cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" 
        TabStripPlacement="Top" ActiveTabIndex="0" AutoPostBack="false">
<cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="外协表单" TabIndex="0">
<ContentTemplate>
<div class="box-wrapper">
 <div style="height:6px" class="box_top"></div>
    <div class="box-outer">
        <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <tr>
            <td style=" font-size:large; text-align:center;" colspan="7">
                外协制作、加工联系单
            </td>
                <td align="right">
                  <asp:Image ID="Image3" ToolTip="返回上一页"  CssClass="hand"  Height="16" Width="16" runat="server" onclick="history.go(-1);" ImageUrl="~/Assets/icons/back.png" />
                </td>        
        </tr>
        <tr>
            <td style="width:8%" align="right">生产制号:</td>
            <td style="width:15%"><asp:Label ID="tsa_id" runat="server" Width="100%"/>
            <input ID="proid" runat="server" readonly="readonly" style="display: none"  type="text"></input>
            </td> 
            <td align="right" style="width:8%">项目名称:</td>
                <td style="width:14%">
                    <asp:Label ID="lab_proname" runat="server" Width="100%"></asp:Label>
                </td>                
                <td align="right" style="width:8%">
                    工程名称:</td>
                <td style="width:14%">
                    <input ID="txt_PlanType" runat="server" type="text" readonly="readonly" style="display: none" />
                    <asp:Label ID="lab_engname" runat="server" Width="100%"></asp:Label><input ID="eng_type" runat="server" type="text" readonly="readonly" style="display: none" />
                </td>  
            <td style="width:8%" align="right">计划编号:</td>
            <td style="width:25%"><asp:Label ID="osl_no1" runat="server" Width="100%"/></td>
               
          
         </tr>
        <tr>
            <td style="width:8%" align="right">批准日期:</td>
            <td style="width:15%" colspan="3"><asp:Label ID="txt_approval" runat="server" Width="100%"/></td>
            <td style="width:8%" align="right">编制日期:</td>
            <td style="width:15%" colspan="2"><asp:Label ID="txt_plandate" runat="server" Width="100%"/></td> 
            <td align="center">
               <asp:HyperLink ID="hpView" CssClass="link" Target="_blank" Visible="false" runat="server">查看变更前记录
               <asp:Image ID="ImageTo" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" 
                            align="absmiddle" runat="server" /></asp:HyperLink>
           </td>  
        </tr>
       <tr>
           <td align="right">
               <asp:Label ID="lblNote" runat="server" Text=""> </asp:Label></td>
           <td colspan="3" >
               <asp:TextBox ID="txtBZ" Width="400px" runat="server" TextMode="MultiLine"></asp:TextBox></td>
            <td colspan="4">   
                  查询类别:<asp:DropDownList ID="ddlQueryType" runat="server">
                   <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
                   <asp:ListItem Text="物料编码" Value="OSL_MARID"></asp:ListItem>
                   <asp:ListItem Text="物料名称" Value="OSL_NAME"></asp:ListItem>
                   <asp:ListItem Text="物料规格" Value="OSL_GUIGE"></asp:ListItem>
                   <asp:ListItem Text="材质" Value="OSL_CAIZHI"></asp:ListItem>
                   <asp:ListItem Text="计划跟踪号" Value="OSL_TRACKNUM"></asp:ListItem>
                  </asp:DropDownList>&nbsp;&nbsp;<asp:TextBox ID="txtQueryText" runat="server"></asp:TextBox> &nbsp;&nbsp;
                   <asp:Button ID="btnQuery" runat="server" Text="查 询" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;
                   <asp:Button ID="btnOrderStateShow" runat="server" Text="显示订单状态" OnClick="btnOrderStateShow_OnClick" />
               </td> 
           </tr>
        </table>
    </div>
<div class="box-outer">

 <asp:Label ID="lblAfter" runat="server" Font-Bold="true" Visible="false" ForeColor="Red"  Text="本批变更增加或减少物料计划"></asp:Label>
<asp:Panel ID="Panel2" Width="100%"  runat="server"><div style="text-align:center; font-size:medium;"><br />没有记录!</div></asp:Panel>
            <yyc:SmartGridView ID="SmartGridView1" width="100%" OnRowDataBound="GridView1_RowDataBound"  CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333">
                <FixRowColumn FixRowType="Header,Pager" TableHeight="150px" TableWidth="100%" FixColumns="0" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                 <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                    <asp:BoundField DataField="OSL_MARID" HeaderText="物料编码"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_NAME" HeaderText="部件名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_BIAOSHINO" HeaderText="标识" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_KU" HeaderText="库" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_GUIGE" HeaderText="规格"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_CAIZHI" HeaderText="材质"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_STANDARD" HeaderText="国标"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_UNITWGHT" HeaderText="单重(kg)"  DataFormatString="{0:F2}"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_NUMBER" HeaderText="数量"  DataFormatString="{0:F2}"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_TOTALWGHTL" HeaderText="总重(kg)"  DataFormatString="{0:F2}"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_UNIT" HeaderText="采购单位"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_WDEPNAME" HeaderText="外委部门"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_REQUEST" HeaderText="加工要求"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"  />
                  <asp:BoundField DataField="OSL_REQDATE" HeaderText="加工日期"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"  />
                <asp:BoundField DataField="OSL_DELSITE" HeaderText="交货地点" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_NOTE" HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"  />
                <asp:BoundField DataField="OSL_TRACKNUM" HeaderText="计划跟踪号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" />                            
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </yyc:SmartGridView>


<asp:Label ID="lblBefore" runat="server" Font-Bold="true" ForeColor="Red"  Visible="false" Text="变更后计划"></asp:Label>
<asp:Panel ID="NoDataPanel" Width="100%"  runat="server"><div style="text-align:center; font-size:medium;"><br />没有记录!</div></asp:Panel>
            <yyc:SmartGridView ID="GridView1" width="100%" OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333">
                <FixRowColumn FixRowType="Header,Pager" TableHeight="450px" TableWidth="100%" FixColumns="0" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                 <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="变更信息" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                   <ItemTemplate>
                         <asp:HyperLink ID="hplBeforeChg" CssClass="hand" ToolTip='<%#Eval("OSL_NEWXUHAO")+"-点击查看变更前信息" %>' onclick="ShowDetail(this.title);" runat="server"><asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                         <asp:Label ID="lblChgState" runat="server" Text='<%#Eval("OSL_STATUS").ToString()=="1"?"删除":Eval("OSL_STATUS").ToString()=="2"?"增加":Eval("OSL_STATUS").ToString()=="3"?"修改":"" %>'></asp:Label>
                         </asp:HyperLink>  
                         <cc1:PopupControlExtender
                            ID="PopupControlExtender1" 
                            runat="server" 
                            DynamicServiceMethod="GetOutDynamicContent"
                            DynamicContextKey='<%# Eval("OSL_NEWXUHAO") %>'
                            DynamicControlID="Panel1"
                            TargetControlID="hplBeforeChg"
                            PopupControlID="Panel1"
                            Position="Right" OffsetY="25">
                        </cc1:PopupControlExtender>
                   </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="到货情况" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                     <ItemTemplate>
                           <asp:HyperLink ID="hplAOG" CssClass="hand" runat="server"><asp:Image ID="AddImage12" ImageUrl="~/Assets/icons/dindan.jpeg" Height="16" Width="16" border="0" hspace="2" align="absmiddle" runat="server" />
                               <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                           </asp:HyperLink> 
                            <cc1:PopupControlExtender
                                ID="PopupControlExtender2" 
                                runat="server" 
                                DynamicServiceMethod="GetAOGOutDynamicContent"
                                DynamicContextKey='<%# Eval("OSL_TRACKNUM") %>'
                                DynamicControlID="Panel3"
                                TargetControlID="hplAOG"
                                PopupControlID="Panel3"
                                Position="Right" OffsetY="25">
                            </cc1:PopupControlExtender>
                     </ItemTemplate> 
                 </asp:TemplateField>
                    <asp:BoundField DataField="OSL_MARID" HeaderText="物料编码"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_NAME" HeaderText="部件名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_BIAOSHINO" HeaderText="标识" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_KU" HeaderText="库" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_GUIGE" HeaderText="规格"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_CAIZHI" HeaderText="材质"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_STANDARD" HeaderText="国标"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_UNITWGHT" HeaderText="单重(kg)"  DataFormatString="{0:F2}"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_NUMBER" HeaderText="数量"  DataFormatString="{0:F2}"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_TOTALWGHTL" HeaderText="总重(kg)"  DataFormatString="{0:F2}"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_UNIT" HeaderText="采购单位"   HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="OSL_WDEPNAME" HeaderText="外委部门"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="OSL_REQUEST" HeaderText="加工要求"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"  />
                  <asp:BoundField DataField="OSL_REQDATE" HeaderText="加工日期"  HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"  />
                <asp:BoundField DataField="OSL_DELSITE" HeaderText="交货地点" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_NOTE" HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"  />
                <asp:BoundField DataField="OSL_TRACKNUM" HeaderText="计划跟踪号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" />                 
                <asp:TemplateField HeaderText="隐藏列" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" Visible="false" >
                    <ItemTemplate>
                        <asp:Label ID="lbltasknum" runat="server" Text='<%#Eval("OSL_TRACKNUM") %>' ></asp:Label>
                        <asp:Label ID="lblkeycoms" runat="server" Text='<%#Eval("OSL_KEYCOMS") %>' ></asp:Label>
                        <asp:Label ID="lbllength" runat="server" Text='<%#Eval("OSL_LENGTH") %>'></asp:Label>
                        <asp:Label ID="lblwidth" runat="server" Text='<%#Eval("OSL_WIDTH") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>             
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </yyc:SmartGridView>
        <asp:Panel ID="Panel1" runat="server">
        </asp:Panel>
       <asp:Panel ID="Panel3" runat="server">
      </asp:Panel>
</div>
</div>
</ContentTemplate>  
</cc1:TabPanel>
<cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="审  核" TabIndex="1">
<ContentTemplate>
   <div class="box-wrapper">
   <div style="height:6px" class="box_top"></div>
   <div class="box-outer">
    <table width="100%">
         <tr>
            <td>
            <input id="btnOutsubmit"  type="button" value="提 交" onclick="return Submit();" />
            <asp:Button ID="btnsubmit" runat="server" CssClass="hidden" onclick="btnsubmit_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnreturn" runat="server" Text="返 回" onclick="btnreturn_Click"  />
            </td>
            <td align="center">
                                <asp:CheckBox ID="ckbMessage" Checked="true" runat="server" />&nbsp;Notes邮件提醒</td>
            <td align="right">
            <asp:RadioButtonList ID="rblSHJS" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
              <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
              <asp:ListItem Text="二级审核" Value="2"></asp:ListItem>
              <asp:ListItem Text="三级审核" Value="3"></asp:ListItem>
              </asp:RadioButtonList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" style="font-size:large; text-align:center; height:43px;">
                外协制作、加工联系单
                <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif" Visible="false"/>
            </td>
        </tr>
    </table>
    </div>
    <div class="box-outer">
        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
            <tr>
                <td style="width:10%" align="center">项目名称</td>
                <td style="width:40%"><asp:Label ID="proname" runat="server" Width="100%"/></td>
                <td style="width:10%" align="center">工程名称</td>
                <td style="width:40%"><asp:Label ID="engname" runat="server" Width="100%"/></td>
            </tr>
            <tr>
                <td align="center">编制</td>
                <td><asp:Label ID="txt_editor" runat="server" Width="100%"></asp:Label></td>
                <input id="editorid" type="text" runat="server" readonly="readonly" style="display:none" />
                <td align="center">编制日期</td>
                <td><asp:Label ID="plandate" runat="server" Width="100%"/></td>   
            </tr>
            <tr>
                <td align="center">主管审核</td>
                <td colspan="3">
                <table width="100%" cellpadding="4" cellspacing="1" border="1">
                    <tr style="height: 25px">
                    <td align="center" style="width:10%">审批人</td>
                    <td style="width:20%">
                        <asp:TextBox ID="txt_first" runat="server" Width="80px" Enabled="false" ></asp:TextBox>
                        <input id="firstid" type="text" runat="server" readonly="readonly" style="display:none" />
                        <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelTechPersons1()" Visible="false" >
                        <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle" runat="server"/>
                                选择
                        </asp:HyperLink>
                    </td>
                    <td align="center" style="width:10%">审核结论</td>
                    <td align="center" style="width:20%">
                        <asp:RadioButtonList ID="rblfirst" RepeatColumns="2" OnSelectedIndexChanged="rblfirst_OnSelectedIndexChanged" AutoPostBack="true" runat="server" Height="20px" Enabled="false">                
                            <asp:ListItem Text="同意" value="4" ></asp:ListItem>
                            <asp:ListItem Text="不同意" Value="3" ></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="center" style="width:10%">审核时间</td>
                    <td style="width:20%">
                        <asp:Label ID="first_time" runat="server" Width="100%"></asp:Label>
                    </td>
                    </tr>
                    <tr>
                    <td colspan="6">
                        <asp:TextBox ID="first_opinion" runat="server" TextMode="MultiLine" Width="100%" Height="42px" Enabled="false"></asp:TextBox>
                    </td>
                    </tr>
                </table> 
                </td>
            </tr>
            <tr>
                <td align="center">部门领导</td>
                <td colspan="3">
                <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                <tr>
                    <td align="center" style="width:10%">审批人</td>
                    <td style="width:20%">
                        <asp:TextBox ID="txt_second" runat="server" Width="80px" Enabled="false" ></asp:TextBox>
                        <input id="secondid" type="text" runat="server" readonly="readonly" style="display:none" />
                        <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand" onClick="SelTechPersons2()" Visible="false"  ><asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                                选择
                    </asp:HyperLink>
                    </td>
                    <td align="center" style="width:10%">审核结论</td>
                    <td align="center" style="width:20%">
                    <asp:RadioButtonList ID="rblsecond" RepeatColumns="2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblsecond_OnSelectedIndexChanged" Height="20px" Enabled="false" >                
                        <asp:ListItem Text="同意" value="6" ></asp:ListItem>
                        <asp:ListItem Text="不同意" Value="5" ></asp:ListItem>
                    </asp:RadioButtonList>
                    </td>
                    <td align="center" style="width:10%">审核时间</td>
                    <td style="width:20%">
                        <asp:Label ID="second_time" runat="server" Width="100%"></asp:Label>
                    </td>
                </tr>
                    <tr>
                        <td colspan="6">
                            <asp:TextBox ID="second_opinion" runat="server" Height="42px" 
                                TextMode="MultiLine" Width="100%" Enabled="false" ></asp:TextBox>
                        </td>
                    </tr>
                </table>
                </td>
            </tr>
            <tr>
                <td align="center">主管经理</td>
                <td colspan="3">
                <table  align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                <tr>
                <td align="center" style="width:10%">审批人</td>
                <td style="width:20%">
                    <asp:TextBox ID="txt_third" runat="server" Width="80px" Enabled="false" ></asp:TextBox>
                    <input id="thirdid" type="text" runat="server" readonly="readonly" style="display:none" />
                    <asp:HyperLink ID="hlSelect3" runat="server" CssClass="hand" onClick="SelTechPersons3()" Visible="false"  ><asp:Image ID="AddImage3" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                    选择
                    </asp:HyperLink>
                </td>
                <td align="center" style="width:10%">审核结论</td>
                <td align="center" style="width:20%">
                    <asp:RadioButtonList ID="rblthird" RepeatColumns="2" AutoPostBack="true" OnSelectedIndexChanged="rblthird_OnSelectedIndexChanged" runat="server" Height="20px" Enabled="false" >                
                        <asp:ListItem Text="同意" value="8" ></asp:ListItem>
                        <asp:ListItem Text="不同意" Value="7" ></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td align="center" style="width:10%">审核时间</td>
                <td style="width:20%">
                    <asp:Label ID="third_time" runat="server" Width="100%"></asp:Label>
                </td>
                </tr>
                <tr>
                <td colspan="6">
                    <asp:TextBox ID="third_opinion" runat="server" TextMode="MultiLine" Width="100%" Height="42px" Enabled="false" ></asp:TextBox>
                </td>
                </tr>
                </table>
                </td>
            </tr>
        </table>
        <asp:Label ID="lblStatus" runat="server" Visible="false" Text=""></asp:Label>
    </div>
   </div> 
</ContentTemplate>
</cc1:TabPanel>  
</cc1:TabContainer>
    </ContentTemplate>
</asp:UpdatePanel>
 <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
<ProgressTemplate>
       <div style="position: absolute; top: 50%; right:40%">
       <table>
       <tr>
       <td align="right"><asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" /></td>
       <td align="left" style="background-color:Yellow; font-size:medium;">数据处理中，请稍后...</td>
       </tr>
       </table>
       </div>
</ProgressTemplate>
</asp:UpdateProgress>
</asp:Content>

