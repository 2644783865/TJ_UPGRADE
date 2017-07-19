<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MT_Menu.aspx.cs" Inherits="ZCZJ_DPF.MT_Data.MT_Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
</head>
<body id="leftMenu">
    <form id="form1" runat="server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <script language="javascript" type="text/javascript">
    function SelectMenu(num)
    {
    for(var i=1;i<=16;i++)
    {
    if(document.getElementById("HyperLink"+i)!=null)
        document.getElementById("HyperLink"+i).className='LeftMenuNoSelected';
    }
    if(document.getElementById("HyperLink"+num)!=null)
        document.getElementById("HyperLink"+num).className='LeftMenuSelected';
    }
    </script>
    
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="Timer1" Interval="30000" runat="server">
    </asp:Timer>
   <div id="menu">
        <div id="menuTitle">
         功能选项<asp:Label ID="ControlFinder" runat="server" Visible="false" ></asp:Label></div>
         <div id="menuContent">
         
             <asp:HyperLink ID="HyperLink1" onClick="SelectMenu(1);" Target="_blank"  CssClass="LeftMenuNoSelected"
             runat="server">
             <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <Triggers>
                   <asp:AsyncPostBackTrigger  ControlID="Timer1" EventName="Tick"/>
                </Triggers>
                 <ContentTemplate><p>投标评审<asp:Label ID="lb_toubiao" runat="server" ForeColor="Red"></asp:Label></p>
                 </ContentTemplate>
                </asp:UpdatePanel>        
             </asp:HyperLink>
             
           <asp:HyperLink ID="HyperLink2" onClick="SelectMenu(2);" Target="_blank"  CssClass="LeftMenuNoSelected"
              runat="server">              
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                     <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick"/>
                    </Triggers>
                    <ContentTemplate>
                    <p>技术审核任务<asp:Label ID="task" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                    </asp:UpdatePanel>
              </asp:HyperLink>              
              
             <asp:HyperLink ID="HyperLink3"  onClick="SelectMenu(3);" Target="_blank" CssClass="LeftMenuNoSelected"
             runat="server">
              <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                     <Triggers><asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick"/> </Triggers>
                    <ContentTemplate>
                    <p>采购申请审批<asp:Label ID="lbl_cgsp" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                    </asp:UpdatePanel>
             </asp:HyperLink>             
             
             
             <asp:HyperLink ID="HyperLink4" onClick="SelectMenu(4);" Target="_blank" CssClass="LeftMenuNoSelected"
               runat="server">
              <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                <Triggers><asp:AsyncPostBackTrigger  ControlID="Timer1" EventName="Tick"/></Triggers>
                <ContentTemplate>
                <p>需用计划管理<asp:Label ID="lb_XYplan" runat="server" Text="" ForeColor="Red"></asp:Label></p> 
                </ContentTemplate>
               </asp:UpdatePanel> 
             </asp:HyperLink>             
             
             
           <asp:HyperLink ID="HyperLink5"  onClick="SelectMenu(5);" Target="_blank" CssClass="LeftMenuNoSelected"
             runat="server">
             <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                <Triggers><asp:AsyncPostBackTrigger  ControlID="Timer1" EventName="Tick"/></Triggers>
                <ContentTemplate>      
                 <p>物料占用管理<asp:Label ID="lb_wlzygl" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                  </ContentTemplate>
               </asp:UpdatePanel> 
             </asp:HyperLink>
                          
             
             <asp:HyperLink ID="HyperLink6" onClick="SelectMenu(6);" Target="_blank" CssClass="LeftMenuNoSelected"
             runat="server">
               <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <Triggers><asp:AsyncPostBackTrigger  ControlID="Timer1" EventName="Tick"/></Triggers>
                <ContentTemplate>  
                <p>比价单管理<asp:Label ID="lb_bjdsh" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
               </asp:UpdatePanel>             
             </asp:HyperLink>     
             
             <asp:HyperLink ID="HyperLink16"  onClick="SelectMenu(16);" Target="_blank" CssClass="LeftMenuNoSelected"
             runat="server">
               <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                <Triggers><asp:AsyncPostBackTrigger  ControlID="Timer1" EventName="Tick"/></Triggers>
                <ContentTemplate>  
                <p>代用单管理<asp:Label ID="lb_dydsh" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
               </asp:UpdatePanel>             
             </asp:HyperLink>     
             
             <asp:HyperLink ID="HyperLink7"  onClick="SelectMenu(7);" Target="_blank" CssClass="LeftMenuNoSelected"
             runat="server">
              <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                <Triggers><asp:AsyncPostBackTrigger  ControlID="Timer1" EventName="Tick"/></Triggers>
                <ContentTemplate>  
                <p>采购变更管理 <asp:Label ID="lb_biangeng" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                 </ContentTemplate>
                </asp:UpdatePanel>              
             </asp:HyperLink>
                          
             
             <asp:HyperLink ID="HyperLink11"  onClick="SelectMenu(11);" Target="_blank" CssClass="LeftMenuNoSelected"
             runat="server">
               <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <Triggers>
                   <asp:AsyncPostBackTrigger  ControlID="Timer1" EventName="Tick"/>
                </Triggers>
                 <ContentTemplate><p>报检任务管理<asp:Label ID="lb_task_baojian" runat="server" ForeColor="Red"></asp:Label></p>
                 </ContentTemplate>
                </asp:UpdatePanel>             
             </asp:HyperLink>             
             
             <asp:HyperLink ID="HyperLink9"  onClick="SelectMenu(9);" Target="_blank" CssClass="LeftMenuNoSelected"
             runat="server">
              <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                <Triggers>
                   <asp:AsyncPostBackTrigger  ControlID="Timer1" EventName="Tick"/>
                </Triggers>
                 <ContentTemplate><p>我的报检任务<asp:Label ID="lb_baojian" runat="server" ForeColor="Red"></asp:Label></p>
                 </ContentTemplate>
                </asp:UpdatePanel>             
             </asp:HyperLink>             
             
             
              <asp:HyperLink ID="HyperLink10"  onClick="SelectMenu(10);" Target="_blank" CssClass="LeftMenuNoSelected"
             runat="server">
              <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                     <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick"/>
                    </Triggers>
                    <ContentTemplate>
                    <p>不合格品通知单<asp:Label ID="lb_rejectPro" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                    </asp:UpdatePanel>             
             </asp:HyperLink>             
                     
             <asp:HyperLink ID="HyperLink12"  onClick="SelectMenu(12);" Target="_blank" CssClass="LeftMenuNoSelected"
             runat="server">
             <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                     <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick"/>
                    </Triggers>
                    <ContentTemplate>
                    <p>报废通知单<asp:Label ID="lb_discardpro" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                    </asp:UpdatePanel>             
             </asp:HyperLink>
                          
             
             <asp:HyperLink ID="HyperLink13" onClick="SelectMenu(13);" Target="_blank" CssClass="LeftMenuNoSelected"
             runat="server">
             <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                     <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick"/>
                    </Triggers>
                    <ContentTemplate>
                    <p>厂商审批<asp:Label ID="CUSUP_REVIEW" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                    </asp:UpdatePanel>      
             </asp:HyperLink>             
             
             
             <asp:HyperLink ID="HyperLink14"  onClick="SelectMenu(14);" Target="_blank" CssClass="LeftMenuNoSelected"
             runat="server">             
             <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                     <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick"/>
                    </Triggers>
                    <ContentTemplate>
                    <p>合同审批任务<asp:Label ID="MyViewTask" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                    </asp:UpdatePanel>      
             </asp:HyperLink>
             
             
             <asp:HyperLink ID="HyperLink15"  onClick="SelectMenu(15);" Target="_blank" CssClass="LeftMenuNoSelected"
             runat="server">
                <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                     <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick"/>
                    </Triggers>
                    <ContentTemplate>
                    <p>待办款项<asp:Label ID="Undo_QK" runat="server" ForeColor="Red"></asp:Label>
                    <asp:Label ID="Undo_YK" runat="server" ForeColor="Red"></asp:Label>
                    </p>
                    </ContentTemplate>
                    </asp:UpdatePanel>
             
             </asp:HyperLink>
              <asp:HyperLink ID="HyperLink8" onClick="SelectMenu(16);" Target="_blank"  CssClass="LeftMenuNoSelected"
             runat="server">
             <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
                <Triggers>
                   <asp:AsyncPostBackTrigger  ControlID="Timer1" EventName="Tick"/>
                </Triggers>
                 <ContentTemplate><p>部门完工确认<asp:Label ID="tisknotice" runat="server" ForeColor="Red"></asp:Label></p>
                 </ContentTemplate>
                </asp:UpdatePanel>        
             </asp:HyperLink>
             <asp:HyperLink ID="HyperLink17" runat="server" onClick="SelectMenu(17);" CssClass="LeftMenuNoSelected" Target="_blank">
                <asp:UpdatePanel ID="UpdatePanel17" runat="server" UpdateMode="Conditional">
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate> <p>售后服务申请及审批<asp:Label ID="lb_shfw" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
                </asp:UpdatePanel>
               </asp:HyperLink>
    </div>
    </div>
    </form>
</body>
</html>
